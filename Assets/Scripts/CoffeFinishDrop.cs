using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffeeFinishDrop : Drop {

    public List<int> acceptedStates;
    
    public int GameScene;

    public int CoffeCounter = 0;
    
    public override void OnDrop(Drag drag) {
        if (drag.Type == "Cup") {
            var state = drag.GetComponent<StateBehaviour>();
            if (acceptedStates.Contains(state.CurrentState)) {
                if (++CoffeCounter >= 3) {
                    SceneManager.LoadScene(GameScene);
                }
                state.SetState(0);
                drag.MoveTo(new Vector2(3, -2));
            }
        }
    }
}
