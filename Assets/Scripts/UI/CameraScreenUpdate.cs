using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CameraScreenUpdate : MonoBehaviour {
    
    private TextMeshProUGUI[] textArr;

    private void Start() {
        if(!GameManager.isHacker) {
            Destroy(this.gameObject);   
        }

        textArr = GetComponentsInChildren<TextMeshProUGUI>();   
        Debug.Log(textArr.Length);
    }

    private void FixedUpdate() {
        textArr[1].text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }

    public void SetCamNumber(int camIdx) {
        textArr[0].text = "CAM " + camIdx;
    }
}
