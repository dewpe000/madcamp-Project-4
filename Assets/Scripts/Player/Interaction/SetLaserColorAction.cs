using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SetLaserColorAction : ActionForPlayer
{

    public ShootLaser laserPointer;
    private bool isOn;
    
    void Start() {
        isOn = false;
    }

    void Update() {

    }

    public override void Action() {
        Debug.Log("Action!");
        laserPointer.SetLaserTransparent(isOn, true);
        isOn = !isOn;

    }



}
