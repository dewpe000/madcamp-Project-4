using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public float InteractionRange = 5f;
    private Camera cam; 

    private void Start() {
        cam = GameObject.Find("Brain Camera").GetComponent<Camera>();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, InteractionRange)) {
                Debug.Log("hitInfo name: " + hitInfo.collider.name);
                if(hitInfo.collider.gameObject.GetComponent<ActionForPlayer>() != null) {
                    hitInfo.collider.gameObject.GetComponent<ActionForPlayer>().Action();
                }
            }
        }   
    }
}