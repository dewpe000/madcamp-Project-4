using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateBlockAction : ActionForPlayer {

    public Transform player;

    private Transform holder;
    public float rotateDegree = 45f;
    public int rotateAxis;
    private float destAngle;
    private bool isRotating;

    private int rotateMode;
    private float curTime;

    private PhotonView PV;

    private void Start() {
        holder = transform.parent;
        isRotating = false;
        PV = photonView;
    }

    private void Update() {
        if(isSelected && !isRotating) {
            if(Input.GetKeyDown(KeyCode.Q)) {
                PV.RPC("Rotate", RpcTarget.All, true, 1);
            }
            if(Input.GetKeyDown(KeyCode.E)) {
                PV.RPC("Rotate", RpcTarget.All, true, -1);
            }
        }

        if(isRotating) {
            Rotate(isRotating, rotateMode);
        } 
        else {
            AngleCorrection();
        }
    }

    private void FixedUpdate() {
        if(isSelected) {
            Deselect();
        }
    }

    public override void Action() {
        if(!isSelected) {
            PV.RPC("ActionPun", RpcTarget.All, true);
        }
    }

    [PunRPC]
    private void ActionPun(bool isSelected) {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.isSelected = isSelected;
    }

    private void Deselect() {
        if(Vector3.Distance(player.transform.position, transform.position) > 5f) {
            isSelected = false;
        }
    }

    [PunRPC]
    private void Rotate(bool isRotating, int rotateMode) {
        this.isRotating = isRotating;
        this.rotateMode = rotateMode;

        curTime += Time.deltaTime;

        if(curTime < 1) {
            if(rotateAxis == 0) {
                transform.RotateAround(holder.position, holder.right, rotateMode * rotateDegree * Time.deltaTime);
            } else if(rotateAxis == 1) {
                transform.RotateAround(holder.position, holder.up, rotateMode * rotateDegree * Time.deltaTime);
            } else if(rotateAxis == 2) {
                transform.RotateAround(holder.position, holder.forward, rotateMode * rotateDegree * Time.deltaTime);
            }
        }
        else {
            AngleCorrection();
            this.isRotating = false;
            curTime = 0;
        }
    }

    private void AngleCorrection() {
        if(rotateAxis == 0) {
            float temp = Mathf.Round(transform.eulerAngles.x / rotateDegree) * rotateDegree;
            transform.rotation = Quaternion.Euler(temp, transform.eulerAngles.y, transform.eulerAngles.z);
        } else if(rotateAxis == 1) {
            float temp = Mathf.Round(transform.eulerAngles.y / rotateDegree) * rotateDegree;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, temp, transform.eulerAngles.z);
        } else if(rotateAxis == 2) {
            float temp = Mathf.Round(transform.eulerAngles.z / rotateDegree) * rotateDegree;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, temp);            
        }
    }

}
