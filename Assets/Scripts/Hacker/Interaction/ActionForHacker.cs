using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Chat;
using Photon.Realtime;
using ExitGames.Client.Photon;

public abstract class ActionForHacker : MonoBehaviourPun {
    protected bool isSelected;

    public abstract void Action();
    public abstract void Stop();
}
