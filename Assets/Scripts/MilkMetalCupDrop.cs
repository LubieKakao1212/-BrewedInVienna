using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MilkMetalCupDrop : Drop {
    
    [SerializeField]
    private List<Conversion> conversions;

    public override void OnDrop(Drag drag) {
        var state = GetComponent<StateBehaviour>();
        var otherState = drag.GetComponent<StateBehaviour>();
        var conversion = conversions.Find(c => state.CurrentState == c.thisStateIn && c.type == drag.Type && otherState.CurrentState == c.otherStateIn);
        
        if (conversion != null) {
            state.SetState(conversion.thisStateOut);
            otherState.SetState(conversion.otherStateOut);
        }
    }

    [Serializable]
    public class Conversion {
        public string type;
        public int thisStateOut;
        public int thisStateIn;
        public int otherStateIn;
        public int otherStateOut;
    }
}

