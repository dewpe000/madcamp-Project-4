using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlateAction : MirrorAction {
    
    MirrorAction mirror;
    void Start() {
        mirror = transform.parent.gameObject.GetComponent<MirrorAction>();
    }
    
    public override void Action() {
        mirror.Action();
    }

    public override void Deselect() {
        mirror.Deselect();
    }
}
