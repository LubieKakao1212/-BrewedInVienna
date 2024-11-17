using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MilkMetalCupDrop : Drop {
    
    [SerializeField]
    private List<Conversion> conversions;

    public override void OnDrop(Drag drag) {
        var conversion = conversions.Find((c) => c.type == drag.Type);

        if (conversion != null) {
            GetComponent<StateBehaviour>().SetState(conversion.state);
        }
    }

    [Serializable]
    public class Conversion {
        public string type;
        public int state;
    }
}

