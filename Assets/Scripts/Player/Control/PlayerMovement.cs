using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {       
    private  CharacterController controller;
    private Vector3 velocity;

    //For Move
    public float[] speedList;
    private float moveSpeed;
    private enum PlayerState { walking, sprinting, air}
    private PlayerState state;

    //For Jump
    public float jumpHeight = 3f;
    private float gravity = -9.81f * 3f;


    //For Ground Check
    private float playerHeight;
    private bool onGround;

    void Start() {
        controller = GetComponent<CharacterController>();
        playerHeight = GetComponent<Renderer>().bounds.size.y;
        onGround = false;
    }

   void Update() {
        CheckGround();
        SetState();
        Move();
        Jump();
        ApplyGravity();
    }

    private void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveSpeed = speedList[(int) state];

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void CheckGround() {
        int mapLayer = 1 << LayerMask.NameToLayer("Map");
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, mapLayer);
    }

    private void Jump() {
        if(onGround && velocity.y < 0) {
            velocity.y = -2f;
        }

    }

    private void ApplyGravity() {
        if(Input.GetButton("Jump") && onGround) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }



    private void SetState() {
        if(onGround && Input.GetKey(KeyCode.LeftShift)) {
            state = PlayerState.sprinting;
        }
        else if(onGround) {
            state = PlayerState.walking;
        }
        else {
            state = PlayerState.air;
        }
    }

}
