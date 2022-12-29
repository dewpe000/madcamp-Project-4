using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class ActionForPlayer : MonoBehaviourPun
{
    protected bool isSelected;
    public abstract void Action();
}
