using UnityEngine;


public class GlobalState : MonoBehaviour {
    public static int iteration = 0;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
