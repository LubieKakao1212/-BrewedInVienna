using UnityEngine;

public class Drag : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D myRigidbody;
    private bool isDragging = false;
    private bool isSnapped = false;
    private Vector2 offset;

    // Double-click control
    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isSnapped)
            {
                HandleDoubleClick();
            }
            else
            {
                TryStartDrag();
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            DragObject();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            EndDrag();
        }
    }

    void TryStartDrag()
    {
        if (isSnapped) return; 

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDragging = true;
            offset = (Vector2)transform.position - mousePos;
            
        }
    }

    void DragObject()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        myRigidbody.MovePosition(mousePos + offset); // Update position based on mouse
    }

    void EndDrag()
    {
        isDragging = false;

        // Check if object is dropped on a target
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("DropTarget"))
            {
                Drop dropComponent = collider.gameObject.GetComponent<Drop>();

                if (dropComponent != null)
                {
                    // Sadece ShotThinggy hedefe bağlanabilir
                    if (gameObject.name == "ShottThingy")
                    {
                        Debug.Log("Dropped on target!");
                        dropComponent.OnDrop(this.gameObject);
                        isSnapped = true; // Sadece ShotThinggy için "snap" durumu aktif
                    }
                    else
                    {
                        Debug.Log("This object cannot stay on the target!");
                    }
                    return;
                }
            }
        }

        // ShotThinggy değilse, "snapped" durumu aktif olmayacak
        isSnapped = false;
    }

    private void HandleDoubleClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            ReleaseFromSnap();
        }

        lastClickTime = Time.time;
    }

    public void ReleaseFromSnap()
    {
        Debug.Log("Object released from target!");
        isSnapped = false;

    }
}
