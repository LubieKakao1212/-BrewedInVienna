using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class DragManager : MonoBehaviour {

    private static Controls Controls;

    private static bool wasInit;
    
    public static Vector2 MousePositionWorld => Camera.main.ScreenToWorldPoint(Controls.Player.MousePos.ReadValue<Vector2>());

    public Drag CurrentDrag;
    
    void Start()
    {
        if (wasInit) {
            throw new Exception("Was Already Init");
        }
        Controls = new Controls();
        wasInit = true;
        
        Controls.Enable();

        Controls.Player.Mouseleft.started += _ => TryStartDrag(MousePositionWorld);
        Controls.Player.Mouseleft.canceled += _ => ReleaseDrag(MousePositionWorld);
    }

    private void Update() {
        if (CurrentDrag) {
            CurrentDrag.MoveTo(MousePositionWorld);
        }
    }

    public void TryStartDrag(Vector2 pos) {
        Collider2D[] hits = Physics2D.OverlapPointAll(pos);
        
        foreach (var hit in hits) {
            var drag = hit.GetComponent<Drag>();
            if (drag != null) {
                StartDrag(drag);
                break;
            }
        }
    }

    public void StartDrag(Drag drag) {
        Assert.IsNull(CurrentDrag);
        drag.OnStartDrag();
        CurrentDrag = drag;
    }

    public void ReleaseDrag(Vector2 pos) {
        if (CurrentDrag) {
            Collider2D[] hits = Physics2D.OverlapPointAll(pos);

            foreach (var hit in hits) {
                var drop = hit.GetComponent<Drop>();
                if (drop != null && drop.gameObject != CurrentDrag.gameObject) {
                    drop.OnDrop(CurrentDrag);
                    break;
                }
            }

            CurrentDrag = null;
        }
    }
}
