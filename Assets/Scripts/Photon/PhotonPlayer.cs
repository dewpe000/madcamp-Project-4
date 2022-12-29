using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PhotonPlayer : MonoBehaviourPun
{

    PhotonView PV;
    public CinemachineVirtualCamera playerCam;

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log("fpwejopfjwep");
    }

    void SendEvent() {
    }

    [PunRPC]
    public void RemovePlayerScripts() {
        Debug.Log("Remove Player Scripts");
        Destroy(GetComponent<PlayerDirection>());
        Destroy(playerCam.GetComponent<PlayerInteraction>());
        Destroy(GetComponent<PlayerMovement>());
    } 
}
