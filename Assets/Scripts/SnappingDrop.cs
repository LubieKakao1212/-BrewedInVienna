using System.Collections.Generic;
using UnityEngine;

public class SnappingDrop : Drop
{
    [SerializeField] private Transform targetPosition;

    [SerializeField]
    private List<string> acceptedTypes;
    
    public override void OnDrop(Drag drag) 
    {
        if (acceptedTypes.Contains(drag.Type))
        { 
            drag.MoveTo(targetPosition.position);
            
            Debug.Log($"Object {drag.gameObject.name} successfully connected to {gameObject.name}");
        }
    }

}