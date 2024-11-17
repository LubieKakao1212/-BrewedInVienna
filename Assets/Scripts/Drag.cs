using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool isDragging = false;

    [field: SerializeField] public string Type { get; private set; }

    [SerializeField]
    private Transform offset;

    [SerializeField] private Rigidbody2D rb;
    
    
    public void MoveTo(Vector2 pos) {
        rb.MovePosition(pos + (Vector2)offset.localPosition);
    }


    public virtual void OnStartDrag() {
    }
}
