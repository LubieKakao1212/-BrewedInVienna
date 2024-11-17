using System;
using System.Globalization;
using Dialog;
using UnityEngine;

public class ActivateDialog : MonoBehaviour {
    [SerializeField]
    private DialogManager _dialogs;
    [SerializeField]
    private string _dialogId;
    
    private void OnTriggerEnter2D(Collider2D other) {
        _dialogs.InitDialog(_dialogId + GlobalState.iteration);
    }
}
