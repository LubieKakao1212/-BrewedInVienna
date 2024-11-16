using UnityEngine;

public class MachineLogic : Drop
{
    [SerializeField] private Transform targetPosition;

    public override void OnDrop(GameObject dragtheobject)
    {
        if (dragtheobject.name == "ShottThingy")
        {
            base.OnDrop(dragtheobject);

            if (targetPosition != null)
            {
                dragtheobject.transform.position = targetPosition.position;
            }
            else
            {
                dragtheobject.transform.position = transform.position;
            }

            Rigidbody2D rb = dragtheobject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.MovePosition(targetPosition.position);
            }

            Debug.Log($"Object {dragtheobject.name} successfully connected to {gameObject.name}");
        }
    }

}