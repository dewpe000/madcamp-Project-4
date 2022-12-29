using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MovingMultiBlockAction : ActionForHacker {

    public bool isMode1;
    public float Length;

    private Vector3 vel = Vector3.zero;
    private float smoothTime = 0.06f;

    public List<Transform> transforms1;
    public List<Transform> transforms2;

    private List<Vector3> startPositions1;
    private List<Vector3> startPositions2;

    private List<Vector3> vel1;
    private List<Vector3> vel2;

    PhotonView PV;

    private bool isSelected = false;

    void Awake() {
        // render = GetComponent<Renderer>();
        // originColor = render.material.color;
        PV = photonView;
        isSelected = false;
        startPositions1 = new List<Vector3>();
        startPositions2 = new List<Vector3>();
        vel1 = new List<Vector3>();
        vel2 = new List<Vector3>();

        for (int i = 0; i < transforms1.Count; i++) {
            startPositions1.Add(transforms1[i].position);
            vel1.Add(Vector3.zero);
        }

        for (int i = 0; i < transforms2.Count; i++) {
            startPositions2.Add(transforms2[i].position);
            vel2.Add(Vector3.zero);
        }
    }

    void Update() {
        MoveTransforms();
    }

    public override void Action() {
        if (!isSelected) {
            isMode1 = !isMode1;
            isSelected = true;
            Invoke("SetIsSelectedFalse", 1.0f);
            PV.RPC("ChangeMode", RpcTarget.Others, isMode1);
        }
    }

    private void SetIsSelectedFalse() {
        Debug.Log("qweqwe");
        this.isSelected = false;
    }

    [PunRPC]
    public void ChangeMode(bool isMode1) {
        this.isMode1 = isMode1;
    }

    public override void Stop() {
    }

    private void MoveTransforms() {
        var currentTransforms = transforms1;
        var currentStartPositions = startPositions1;
        var currentVel = vel1;
        var otherTransforms = transforms2;
        var otherStartPositions = startPositions2;
        var otherVel = vel2;

        if (!isMode1) {
            currentTransforms = transforms2;
            currentStartPositions = startPositions2;
            currentVel = vel2;
            otherTransforms = transforms1;
            otherStartPositions = startPositions1;
            otherVel = vel1;
        }

        for (int i = 0; i < currentTransforms.Count; i++) {
            Move(currentTransforms[i], currentStartPositions[i], currentVel[i]);            
        }

		for (int i = 0; i < otherTransforms.Count; i++) {
			BackMove(otherTransforms[i], otherStartPositions[i], otherVel[i]);
		}
	}

    private void Move(Transform tf, Vector3 startPosition, Vector3 vel) {
        if (Vector3.Distance(tf.position, startPosition) < Length) {
            tf.position = Vector3.SmoothDamp(tf.position, tf.position + tf.right, ref vel, smoothTime);
        }
    }

    private void BackMove(Transform tf, Vector3 startPosition, Vector3 vel) {
        if (Vector3.Distance(tf.position, startPosition) > 0.1) {
            tf.position = Vector3.SmoothDamp(tf.position, tf.position - tf.right, ref vel, smoothTime);
        }
    }
}
