using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
    public GameObject trailPrefab;
    GameObject thisTrail;
    Vector3 startPos;
    Plane objPlane;
    public GameObject joyStickPointer;
    public GameObject drawPaper;

    // Start is called before the first frame update
    void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position); // forward 는 z값 0
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    void Draw()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
            Input.GetButtonDown("Fire1"))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
                startPos = mRay.GetPoint(rayDistance);
            thisTrail = (GameObject)Instantiate(trailPrefab,
                startPos, Quaternion.identity);
        }
        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            || Input.GetButton("Fire1")))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
                thisTrail.transform.position = mRay.GetPoint(rayDistance);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
            Input.GetButtonUp("Fire1"))
        {
            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
                Destroy(thisTrail);
        }
    }
}
