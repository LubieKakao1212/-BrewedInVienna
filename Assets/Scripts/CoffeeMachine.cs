using UnityEngine;

public class CoffeeMachine : SnappingDrop {

    public string cupObjType;
    public string coffeeObjType;

    public override void OnDrop(Drag drag) {
        base.OnDrop(drag);
        if (drag.Type == cupObjType) {
            Collider2D[] hits = Physics2D.OverlapPointAll();

            foreach (var hit in hits) {
                var drag2 = hit.GetComponent<Drag>();
                if (drag2 != null && drag2.Type == coffeeObjType) {
                    var coffeeState = drag2.GetComponent<StateBehaviour>();
                    var cupState = drag.GetComponent<StateBehaviour>();
                    if (coffeeState.CurrentState == 2) {
                        if (cupState.CurrentState == 0) {
                            cupState.SetState(1);
                            coffeeState.SetState(0);
                        }
                    }
                    break;
                }
            }   
        }
    }
}
