using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovement : MonoBehaviour
{
    float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            Move(Vector3.up);
        if (Input.GetKey(KeyCode.A))
            Move(Vector3.left);
        if (Input.GetKey(KeyCode.S))
            Move(Vector3.right);
        if (Input.GetKey(KeyCode.D))
            Move(Vector3.down);
    }

    private void Move(Vector3 direction) {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
