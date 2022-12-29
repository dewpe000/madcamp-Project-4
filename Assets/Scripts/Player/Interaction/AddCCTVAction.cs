using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class AddCCTVAction : ActionForPlayer
{

    public byte camNum;

    void Start() {
        isSelected = false;
    }

    void Update() {
        
    }

	public override void Action() {
        Debug.Log("Action!");

        byte evCode = Constant.UNLOCK_CCTV; // Custom Event 0: Used as "MoveUnitsToTargetPosition" event
        object[] content = new object[] { camNum };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);

    }



}
