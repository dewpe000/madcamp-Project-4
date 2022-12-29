using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class HackerCameraManager : MonoBehaviour {
    
    public CinemachineVirtualCamera[] vCamArr;
    public bool[] unlockedArr;
    public int activeCamIdx;

    public Canvas cctvUI;
    private CameraScreenUpdate camUpdate;
    public GameObject camButtonPrefab;

    public float createPosDelta;
    public float createPosX;
    public float createPosY;

    private void Start() {
        //InitVCam();
        camUpdate = cctvUI.GetComponent<CameraScreenUpdate>();
        camUpdate.SetCamNumber(0);
    }

    public void InitVCam() {
        activeCamIdx = 0;
        unlockedArr = new bool[vCamArr.Length];
        if (GameManager.isHacker) {
            vCamArr[0].enabled = true;
            UnlockVCam(0);
        }
    }
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            SwitchNextCam();
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            SwitchPrevCam();
        }
    }

    private void SwitchNextCam() {

        int originIdx = activeCamIdx;
        int nextIdx = (activeCamIdx == vCamArr.Length - 1) ? 0 : activeCamIdx + 1;

        if (unlockedArr[nextIdx] == false) {
            return;
        }

        vCamArr[originIdx].enabled = false;
        vCamArr[nextIdx].enabled = true;
        activeCamIdx = nextIdx;
        camUpdate.SetCamNumber(activeCamIdx);
    }

    private void SwitchPrevCam() {
        int originIdx = activeCamIdx;
        int prevIdx = (activeCamIdx == 0) ? vCamArr.Length - 1 : activeCamIdx - 1;

        if (unlockedArr[prevIdx] == false) {
            return;
        }

        vCamArr[originIdx].enabled = false;
        vCamArr[prevIdx].enabled = true;
        activeCamIdx = prevIdx;
        camUpdate.SetCamNumber(activeCamIdx);

    }

    public void SwitchPickedCam(int pickedCamIdx) {
        if (unlockedArr[pickedCamIdx] == false) {
            Debug.Log("locked!!!");
            return;
        }
        vCamArr[activeCamIdx].enabled = false;
        vCamArr[pickedCamIdx].enabled = true;
        activeCamIdx = pickedCamIdx;
        camUpdate.SetCamNumber(activeCamIdx);
    }

    public void UnlockVCam(int camIdx) {
        AddCamButton(camIdx);
    }

    public void AddCamButton(int camIdx) {
        Debug.Log("Add Cam button!");
        if(camIdx == 0 || !unlockedArr[camIdx]) {
            Debug.Log("camIdx " + camIdx);
            GameObject camButton = (GameObject) Instantiate(camButtonPrefab);
            RectTransform camButtonRect = camButton.GetComponent<RectTransform>();

            camButton.GetComponentInChildren<TextMeshProUGUI>().text =  ""+ camIdx;
            camButton.transform.SetParent(cctvUI.transform);
            camButtonRect.anchorMin = new Vector2(1, 0);
            camButtonRect.anchorMax = new Vector2(1, 0);
            camButtonRect.anchoredPosition = new Vector3(createPosX, createPosY, 0);
            createPosX += createPosDelta;

            if(createPosX / createPosDelta == 5) {
                createPosX = createPosDelta;
                createPosY -= createPosDelta;
            }

            camButton.GetComponent<HackerCameraButton>().SetCamIdx(camIdx);
            unlockedArr[camIdx] = true;
        }
    }
}
