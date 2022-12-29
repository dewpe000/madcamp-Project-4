using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; 
using UnityEngine;

public class HackerInteraction : MonoBehaviour {

    private Camera cam;
    public HackerCameraManager cmManager;

    public MirrorAction curMirror;

    void Start() {
        Cursor.visible = true;
        cam = GetComponent<Camera>();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {

            if(!EventSystem.current.IsPointerOverGameObject()) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if(Physics.Raycast(ray, out hitInfo)) {
                    Debug.Log("Hacker Ray Hit Info " + hitInfo.collider.name);
                    if(hitInfo.collider.gameObject.GetComponent<ActionForHacker>() != null) {

                        if(hitInfo.collider.gameObject.GetComponent<MirrorAction>() != null) {
                            if(curMirror && curMirror != hitInfo.collider.gameObject.GetComponent<MirrorAction>()) {
                                curMirror.Deselect();
                            }
                            curMirror = hitInfo.collider.gameObject.GetComponent<MirrorAction>();
                        }

                        hitInfo.collider.gameObject.GetComponent<ActionForHacker>().Action();
                    }
                }
            }

            } else if (Input.GetMouseButtonUp(0)) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo)) {
                if(hitInfo.collider.gameObject.GetComponent<ActionForHacker>() != null) {
                    hitInfo.collider.gameObject.GetComponent<ActionForHacker>().Stop();
                }
            }
        }
    }
}
