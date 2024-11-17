using System.Collections.Generic;
using UnityEngine;

namespace Dialog {
    public class Dialog {
        public bool HasResponse => playerResponses.Length > 0;
        
        public readonly string speaker;
        public readonly string[] initial;
        public readonly string[] playerResponses;
        public readonly string nextDialog;

        public Dialog(string[] initial, string[] playerResponses, string nextDialog, string speaker) {
            this.initial = initial;
            this.playerResponses = playerResponses;
            this.nextDialog = nextDialog;
            this.speaker = speaker;
        }
    }
}