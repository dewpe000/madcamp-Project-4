using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;

    public string w;
    public string d;

    private Dictionary<string, int> dic = new Dictionary<string, int>() {
        { "X", 0 },
        { "Y", 1 },
        { "Z", 2 },
        { "-X", 3 },
        { "-Y", 4 },
        { "-Z", 5 },};

    private Vector3[] dirs = new Vector3[] { new Vector3(1,0,0),
        new Vector3(0,1,0),
        new Vector3(0,0,1),
        new Vector3(-1,0,0),
        new Vector3(0,-1,0),
        new Vector3(0,0,-1)
    };

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving)
            StartCoroutine(Move(dirs[dic[w]]));
        if (Input.GetKey(KeyCode.A) && !isMoving)
            StartCoroutine(Move(dirs[(dic[d] + 3) % 6]));
        if (Input.GetKey(KeyCode.S) && !isMoving)
            StartCoroutine(Move(dirs[(dic[w] + 3) % 6]));
        if (Input.GetKey(KeyCode.D) && !isMoving)
            StartCoroutine(Move(dirs[dic[d]]));
    }

    private IEnumerator Move(Vector3 direction) {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove) {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

		isMoving = false;

    }

    
}
