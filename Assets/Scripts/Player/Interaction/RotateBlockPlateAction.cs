using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlockPlateAction : RotateBlockAction {

    RotateBlockAction rba;

    private void Start() {
        rba = transform.parent.gameObject.GetComponent<RotateBlockAction>();
    }

    public override void Action()
    {
        rba.Action();
    }
}
