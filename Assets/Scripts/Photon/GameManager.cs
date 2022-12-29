using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Photon.Realtime;
using ExitGames.Client.Photon;


public class GameManager : MonoBehaviour {
    public static bool isHacker = false;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("game start!");

        if (isHacker == false) {
            InitForPlayer();
        } else {
            InitForHacker();
        }
    }

    // Update is called once per frame  
    void Update() {

    }

    private void InitForHacker() {
        Debug.Log("I'm hacker");
        gameObject.GetComponent<HackerCameraManager>().InitVCam();
        PhotonNetwork.AddCallbackTarget(gameObject.AddComponent<PhotonHacker>());
    }

    private void InitForPlayer() {
        Debug.Log("i'm player");
        
        var obj = PhotonNetwork.Instantiate("Prefabs/Player", new Vector3(-12f, 3f, -2.5f), Quaternion.identity);
        obj.GetComponent<PhotonPlayer>().playerCam.enabled = true;
        obj.GetComponent<PhotonView>().RPC("RemovePlayerScripts", RpcTarget.Others);
        Destroy(GetComponent<HackerCameraManager>());
        Destroy(GameObject.FindObjectOfType<HackerInteraction>());
    }

    public void OnDisable() {
        PhotonNetwork.RemoveCallbackTarget(gameObject.GetComponent<PhotonHacker>());
    }

}
