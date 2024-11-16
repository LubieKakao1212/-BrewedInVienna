using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NaughtyAttributes;
using NUnit.Framework;
using TMPEffects.Components;
using TMPro;
using UnityEngine;

namespace Dialog {
    public class DialogManager : MonoBehaviour {
        
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private TMPWriter dialogWriter;
        [SerializeField] private TMPAnimator dialogAnimator;

        [SerializeField]
        private List<TextAsset> dialogAssets;

        private readonly Dictionary<string, Dialog> _dialogs = new();

        private Dialog _currentDialog = null;
        private int _currentDialogSegmentIndex = 0;
        
        [SerializeField] private string debugCurrentDialog;
        
        void Start() {
            ParseDialogs();
        }

        private void ParseDialogs() {
            foreach (var asset in dialogAssets) {
                _dialogs.Add(asset.name, ParseDialog(asset));
            }
        }

        private Dialog ParseDialog(TextAsset asset) {
            var content = asset.text;

            var splitContent = content.Split('|', StringSplitOptions.RemoveEmptyEntries);

            string speaker = null;
            var initial = new List<string>();
            var responses = new List<string>();
            string nextDialog = null;

            bool isInResponses = false;
            
            foreach (var it in splitContent) {
                var section = it;
                if (section.StartsWith("\r\n")) {
                    section = section.Substring(2);
                }
                
                if (speaker == null) {
                    speaker = section;
                    continue;
                }
                
                if (!isInResponses) {
                    if (section.StartsWith("->")) {
                        isInResponses = true;
                    }
                    else {
                        initial.Add(section);
                    }
                }

                if (isInResponses) {
                    if (section.StartsWith("->")) {
                        responses.Add(section);
                    }
                    else {
                        nextDialog = section;
                        break;
                    }
                }
            }

            return new Dialog(initial.ToArray(), responses.ToArray(), nextDialog, speaker);
        }

        private void InitDialog(string dialogName) {
            var dialog = _dialogs[dialogName];
            _currentDialog = dialog;
            _currentDialogSegmentIndex = 0;
            InitNextSegment();
        }

        private void InitNextSegment() {
            if (_currentDialog == null) {
                return;
            }
            var idx = _currentDialogSegmentIndex++;
            if (idx >= _currentDialog.initial.Length) {
                //Init Responses
                if (_currentDialog.HasResponse) {
                    foreach (var response in _currentDialog.playerResponses) {
                        Debug.Log(response);
                    }   
                }
                else {
                    Debug.LogWarning("No Responses");
                }
                return;
            }
            InitSegment(_currentDialog.initial[idx]);
        }
        
        private void InitSegment(string segment) {
            dialogWriter.ResetWriter();
            dialogAnimator.ResetAnimations();
            dialogWriter.StartWriter();
            dialogAnimator.StartAnimating();
            dialogText.text = segment;
        }

        [Button]
        private void SkipNext() {
            if (dialogWriter.IsWriting) {
                dialogWriter.SkipWriter(true);
            }
            else {
                InitNextSegment();
            }
        }
        
        [Button]
        private void DebugInitDialog() {
            InitDialog(debugCurrentDialog);
        }
        
        void Update() {
            
        }
    }
}
