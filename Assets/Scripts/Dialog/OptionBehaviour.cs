using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog {
    public class OptionBehaviour : MonoBehaviour {
    
        [SerializeField] private int index;
    
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private Button button;
    
        public void SetFrom(string text) {
            this.text.SetText(text);
            button.gameObject.SetActive(true);
        }

        public void Hide() {
            button.gameObject.SetActive(false);
        }

        public void Bind(Action<int> action) 
    
        {
            button.onClick.AddListener(() => action(index));
        }
    
    }
}
