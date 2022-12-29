using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSwitchAction : ActionForHacker {

    public Transform tf;
    public bool isDown;
    public float maxHeight;
    public float minHeight;

    // private Vector3 vel = Vector3.zero;
    // private float smoothTime = 0.7f;
    private float scaleUpdate = 0.001f;
    
    void Start() {
        // render = GetComponent<Renderer>();
        // originColor = render.material.color;
        isSelected = false;
    }

    void Update() {
        if (isSelected) {
            if (isDown) {
                DownMove();
            }
            else {
                UpMove();
            } 
        }
    }

    public override void Action() {
        isSelected = true;
        // render.material.color = originColor;
    }

    public override void Stop() {
        isSelected = false;
    }

    private void UpMove() {
        if (tf.localScale.y < maxHeight) {
            tf.localScale = new Vector3(tf.localScale.x, tf.localScale.y + scaleUpdate, tf.localScale.z);
        } else {
            isSelected = false;
        }
    }

    private void DownMove() {
        if (tf.localScale.y > minHeight) {
            tf.localScale = new Vector3(tf.localScale.x, tf.localScale.y - scaleUpdate, tf.localScale.z);
        } else {
            isSelected = false;
        }
    }
}
