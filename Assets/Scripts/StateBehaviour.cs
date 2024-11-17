using UnityEngine;

public class StateBehaviour : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] Sprites;

    public int CurrentState { get; private set; }

    public void SetState(int state) {
        CurrentState = state;
        spriteRenderer.sprite = Sprites[state];
    }
}
