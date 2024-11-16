using UnityEngine;

public class Drag : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D myRigidbody;
    private bool isDragging = false;
    private Vector2 offset;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
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
        myRigidbody.MovePosition(mousePos + offset);
    }

    void EndDrag()
    {
        isDragging = false;

        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("DropTarget"))
            {
                Debug.Log("Dropped on target!");
                SnapToTarget(collider.gameObject);
                return;
            }
        }

        Debug.Log("Dropped, but not on a valid target.");
    }

    private void SnapToTarget(GameObject target)
    {
        
        transform.position = target.transform.position;

        
        GetComponent<SpriteRenderer>().color = Color.green;

        
        Animator draggableAnimator = GetComponent<Animator>();
        if (draggableAnimator != null)
        {
            draggableAnimator.SetTrigger("Dropped");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        Gizmos.DrawWireCube(transform.position, box.size);
    }
}
