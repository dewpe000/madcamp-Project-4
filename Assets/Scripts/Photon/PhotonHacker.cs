using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonHacker : MonoBehaviour, IOnEventCallback {
    // Start is called before the first frame update

    private HackerCameraManager cmManager;

    void Start() {
        Debug.Log("Hacker Init!");

        cmManager = gameObject.GetComponent<HackerCameraManager>();
    }

    // Update is called once per frame
    void Update() {

    }
            
    public void OnEvent(EventData photonEvent) {
        byte eventCode = photonEvent.Code;

        
        var data = photonEvent.CustomData;

        if (data is object[]) {
            var arr = (object[])data;
            switch (eventCode) {
                case Constant.UNLOCK_CCTV:
                    UnlockCCTV((byte)arr[0]);
                    break;
            }
        }
    }

    private void UnlockCCTV(byte camIdx) {
        Debug.Log("Add CCTV! " + camIdx);

        cmManager.UnlockVCam((int) camIdx);
    }


    
}
