using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockAction : ActionForHacker {

    public Transform tf;
    public bool isBack;
    public float Length;

    private Vector3 vel = Vector3.zero;
    private float smoothTime = 0.1f;
    private Vector3 startPosition;
    
    void Start() {
        isSelected = false;
        startPosition = tf.position;
    }

    void Update() {
        if (isSelected) {
            if (isBack) {
                BackMove();
            }
            else {
                Move();
            } 
        }
    }

    public override void Action() {
        isSelected = true;
    }

    public override void Stop() {
        isSelected = false;
    }

    private void Move() {
        if (Vector3.Distance(tf.position, startPosition) < Length) {
            tf.position = Vector3.SmoothDamp(tf.position, tf.position + tf.right, ref vel, smoothTime);
        } else {
            isSelected = false;
        }
    }

    private void BackMove() {
        if (Vector3.Distance(tf.position, startPosition) > 0.1) {
            tf.position = Vector3.SmoothDamp(tf.position, tf.position - tf.right, ref vel, smoothTime);  
        } else {
            isSelected = false;
        }
    }
}
