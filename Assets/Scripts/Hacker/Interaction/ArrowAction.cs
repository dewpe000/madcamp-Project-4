using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAction : ActionForHacker {

    private Vector3 direction;
    private Transform holder;
    private MirrorAction mirrorAction;
    private Vector3 destPos;

    private Vector3 vel = Vector3.zero;
    private float smoothTime = 0.5f;

    private void Start() {
        holder = transform.parent;
        mirrorAction = holder.GetChild(0).GetComponent<MirrorAction>();
    }

    private void Update() {
        if(isSelected) {
            MoveMirror();
        }
    }

    public override void Action() {
        if(!isSelected) {
            destPos = holder.position + direction;
            isSelected = true;
            mirrorAction.SetVisiblityArrows(false);
        }
    }

    public override void Stop() {
        
    }

    public void SetDirection(Vector3 direction) {
        this.direction = direction;
    }

    private void MoveMirror() {
        if(Vector3.Distance(holder.position, destPos) > 0.1) {
            holder.position = Vector3.SmoothDamp(holder.position, destPos, ref vel, smoothTime);
        }
        else {
            isSelected = false;
            holder.position = destPos;
            mirrorAction.DestroyAllArrows();
            mirrorAction.DrawAllArrows();
        }
    }
}
