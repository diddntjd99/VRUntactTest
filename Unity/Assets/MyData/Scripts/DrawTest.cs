using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTest : MonoBehaviour
{
    public GameObject trailPrefab;
    GameObject thisTrail;
    Vector3 startPos;
    //Plane objPlane;
    public GameObject joyStickPointer;
    public GameObject drawPaper;
    RaycastHit hit;
    float MaxDistance = 1000.0f;
    public LayerMask LayerMask;
    public GameObject eraserPrefab;
    Vector3 eraserPos;
    GameObject thisEraser;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Draw();
        Erase();
    }

    void Draw()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || // 버튼을 눌렀을 때 
            Input.GetButtonDown("Fire1"))
        {   
            if (Physics.Raycast(joyStickPointer.transform.localPosition, transform.forward, out hit, MaxDistance, LayerMask))
                startPos = hit.point;
            thisTrail = (GameObject)Instantiate(trailPrefab,
                startPos, Quaternion.identity);
            
            //Debug.Log(startPos);
        }
        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) // 버튼을 누르고 있을 때
            || Input.GetButton("Fire1")))
        {
            if (Physics.Raycast(joyStickPointer.transform.localPosition, transform.forward, out hit, MaxDistance, LayerMask))
                thisTrail.transform.position = hit.point;
            Debug.DrawRay(joyStickPointer.transform.localPosition, transform.forward * 1000, Color.blue, 0.5f);
            //Debug.Log(startPos);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || // 버튼을 뗄 때
            Input.GetButtonUp("Fire1"))
        {
            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
                Destroy(thisTrail);
        }
    }

    void Erase()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(joyStickPointer.transform.localPosition, transform.forward, out hit, MaxDistance, LayerMask))
                eraserPos = hit.point;

            thisEraser = (GameObject)Instantiate(eraserPrefab,
                eraserPos, Quaternion.identity);
        }
    }
}
