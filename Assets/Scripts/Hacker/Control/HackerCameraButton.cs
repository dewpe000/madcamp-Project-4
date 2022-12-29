using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerCameraButton : MonoBehaviour {
    public GameObject cameraManager;
    public HackerCameraManager hcm;
    private int camIdx;
    
    private void Start() {
        cameraManager = GameObject.Find("Event System");
        hcm = cameraManager.GetComponent<HackerCameraManager>();
    }

    public void SetCamIdx(int camIdx) {
        this.camIdx = camIdx;
    }

    public void ClickCamButton() {
        hcm.SwitchPickedCam(camIdx);
    }
}
