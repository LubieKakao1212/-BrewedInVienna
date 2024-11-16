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

        public int LastSelectedOption { get; private set; }

        public bool IsWaitingForOptions {
            get {
                var dialog = _currentDialog;
                if (_currentDialog != null) {
                    if (_currentDialogSegmentIndex == dialog.initial.Length) {
                        if (_currentDialogSegmentIndex > dialog.initial.Length) {
                            Debug.Log("Somethig went wrong during Waiting");
                        }
                        return true;
                    }
                }
                return false;
            }
        }
        
        [SerializeField] private OptionManager options;
        
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private TMPWriter dialogWriter;
        [SerializeField] private TMPAnimator dialogAnimator;

        [SerializeField]
        private List<TextAsset> dialogAssets;

        private readonly Dictionary<string, Dialog> _dialogs = new();

        private Dialog _currentDialog = null;
        private int _currentDialogSegmentIndex = 0;

        private bool isSegmentInit = false;
        
        [SerializeField] private string debugCurrentDialog;
        
        public void SelectOption(int idx) {
            if (!IsWaitingForOptions) {
                Debug.Log("Not Waiting for Option");
                return;
            }
            LastSelectedOption = idx;
            options.Hide();
            EndDialog();
        }

        void Start() {
            ParseDialogs();
            options.Subscribe(SelectOption);
            dialogWriter.OnFinishWriter.AddListener(_ => OnFinishSegment());
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
                return;
            }
            InitSegment(_currentDialog.initial[idx]);
        }

        private void OnFinishSegment() {
            if (!isSegmentInit && IsWaitingForOptions && dialogWriter.CurrentIndex > 0) {
                options.ShowFrom(_currentDialog);
            }
        }
        
        private void InitSegment(string segment) {
            isSegmentInit = true;
            dialogWriter.ResetWriter();
            dialogAnimator.ResetAnimations();
            dialogText.text = segment;
            dialogWriter.StartWriter();
            dialogAnimator.StartAnimating();
            isSegmentInit = false;
        }

        private void EndDialog() {
            var next = _currentDialog.nextDialog;
            if (string.IsNullOrEmpty(next)) {
                return;
            }
            if (!_dialogs.ContainsKey(next)) {
                throw new ApplicationException($"Missing dialog with name {next}");
            }
            InitDialog(next);
        }
        
        [Button]
        private void SkipNext() {
            if (dialogWriter.IsWriting) {
                dialogWriter.SkipWriter();
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
