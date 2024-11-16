using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog {
    public class OptionManager : MonoBehaviour {
        [SerializeField]
        private List<OptionBehaviour> options;
        
        public void Subscribe(Action<int> callback) {
            foreach (var option in options) {
                option.Bind(callback);
            }
        }
        
        public void Hide() {
            foreach (var option in options) {
                option.gameObject.SetActive(false);
            }
        }
        
        public void ShowFrom(Dialog dialog) {
            for (int i = 0; i<Math.Min(options.Count, dialog.playerResponses.Length); i++) {
                options[i].SetFrom(dialog.playerResponses[i]);
            }
        }
    }
}
