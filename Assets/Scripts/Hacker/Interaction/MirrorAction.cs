using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorAction : ActionForHacker {

    public float arrowDistance = 1f;

    private Transform holder;
    private float sensitivity = 120f;

    private List<Vector3> dirList;
    private bool[] isHitArr = {false, false, false, false, false, false};
    private bool canRotate;
    private Vector3 rotateAxis;

    public float rayDistacne = 0.9f;

    public GameObject arrowPrefab;
    private List<GameObject> arrowList;

    private bool isRotating;
    private int rotateMode;
    private float curTime;
    public float rotateDegree = 45f;
    private int hitIdx = 0;


    private void Awake() {
        dirList = new List<Vector3>();
        arrowList = new List<GameObject>();
    }

    void Start() {
        holder = transform.parent;
        dirList.Add(holder.right);
        dirList.Add(holder.right * (-1));
        dirList.Add(holder.up);
        dirList.Add(holder.up * (-1));
        dirList.Add(holder.forward);
        dirList.Add(holder.forward * (-1));

        canRotate = false;
        isSelected = false;
    }

    void Update() {
        
        if(isSelected && canRotate && !isRotating) {
            if(Input.GetKeyDown(KeyCode.R)) {
                rotateMode = 1;
                isRotating = true;
            }
            if(Input.GetKeyDown(KeyCode.T)) {
                rotateMode = -1;
                isRotating = true;
            }
        }

        if(isRotating) {
            Rotate();
        }

    }
    public override void Action() {
        if(!isSelected) {
            isSelected = true;
            DrawAllArrows();
        }
        else {
            isSelected = false;
            if(arrowList.Count != 0) {
                DestroyAllArrows();
            }
        }
    }

    public override void Stop() {
    }

    public  virtual void Deselect() {
        isSelected = false;
        if(arrowList.Count != 0) {
            DestroyAllArrows();
        }
    }

    private void DrawArrow(int dirIdx) {
        GameObject obj = Instantiate(arrowPrefab, holder.position, Quaternion.identity);
        obj.transform.parent = holder;
        obj.transform.position += dirList[dirIdx] * arrowDistance;
        obj.GetComponent<ArrowAction>().SetDirection(dirList[dirIdx]);
        obj.tag = "Arrow";
        arrowList.Add(obj);
    }

    public void DrawAllArrows() {

        Ray ray;
        RaycastHit hitInfo;

        int isHitCount = 0;
        
        for(int i = 0 ; i < isHitArr.Length; i++) {
            isHitArr[i] = false;
        }

        for(int i = 0; i < dirList.Count; i++) {
            ray = new Ray(holder.position, dirList[i]);
            if(Physics.Raycast(ray, out hitInfo, rayDistacne)) {
                Debug.Log(i + " " +hitInfo.collider.name);
                if(hitInfo.collider.gameObject.layer == 3) {
                    isHitArr[i] = true;
                    isHitCount++;
                    hitIdx = i;
                    rotateAxis = (-1) * dirList[i];
                }
                if(hitInfo.collider.gameObject.layer == 8) {
                    isHitArr[i] = true;
                }
            }
        }

        if(isHitCount == 1) {

            canRotate = true;

            if(hitIdx % 2 != 0) {
                hitIdx -= 1;
            }

            for(int i = 0; i < isHitArr.Length; i++) {
                if(i != hitIdx && i != hitIdx + 1 && !isHitArr[i]) {
                    DrawArrow(i);
                }
            }
        }
        else if(isHitCount > 1){
            
            canRotate = false;

            for(int i = 0; i < isHitArr.Length; i++) {
                if(!isHitArr[i]) {
                    DrawArrow(i);
                }
            }
        }
    }


    public void SetVisiblityArrows(bool isVisible) {
         int arrowNum = arrowList.Count;
         for(int i = 0; i < arrowNum; i++) {
             arrowList[i].GetComponent<Renderer>().enabled = isVisible;
         }
    }

    public void DestroyAllArrows() {
        int arrowNum = arrowList.Count;
        GameObject destroyArrow;
        for(int i = 0; i < arrowNum; i++) {
            destroyArrow = arrowList[0];
            arrowList.RemoveAt(0);
            Destroy(destroyArrow);
        }

        for(int i = 0 ; i < isHitArr.Length; i++) {
            isHitArr[i] = false;
        }
    }

    private void Rotate() {

        curTime += Time.deltaTime;

        if(curTime < 1) {
            transform.RotateAround(holder.position, rotateAxis, rotateMode * rotateDegree * Time.deltaTime);
        } else {
            if(hitIdx == 0) {
                float temp = Mathf.Round(transform.eulerAngles.x / rotateDegree) * rotateDegree;
                transform.rotation = Quaternion.Euler(temp, transform.eulerAngles.y, transform.eulerAngles.z);
            } else if(hitIdx == 2) {
                float temp = Mathf.Round(transform.eulerAngles.y / rotateDegree) * rotateDegree;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, temp, transform.eulerAngles.z);
            } else if(hitIdx == 4) {
                float temp = Mathf.Round(transform.eulerAngles.z / rotateDegree) * rotateDegree;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, temp);            
            }
            isRotating = false;
            curTime = 0;
        }
    }
}
