using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour {

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

    public GameObject playerCam;
    private float rotationX;
    private float rotationY;

	private void Awake() {
        if (GameManager.isHacker) {
            Destroy(GetComponent<PlayerDirection>());
        }
	}

	void Start() {
        
        rotationX = 0f;
        rotationY = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update() {

        float mouseMoveX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseMoveY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationX -= mouseMoveY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY += mouseMoveX;

        // transform.rotation = Quaternion.Euler(0, rotationY, 0);
        // playerCam.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.Rotate(Vector3.up * mouseMoveX);
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

    }
}
