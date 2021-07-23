using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseLine : MonoBehaviour
{
    public GameObject joyStickPointer;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    RaycastHit hit;
    float MaxDistance = 1000.0f;
    public LayerMask LayerMask;
    public GameObject eraserPrefab;
    Vector3 eraserPos;
    GameObject thisEraser;

    private DrawLine drawLine;

    // Start is called before the first frame update
    void Start()
    {
        drawLine = this.GetComponent<DrawLine>();
    }

    // Update is called once per frame
    void Update()
    {
        Erase();
    }

    void Erase()
    {
        if (Input.GetButtonDown("Fire1") && drawLine.selectedGameObject != null && drawLine.selectedGameObject.name == "eraser")
        {
            if(joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.name == "DrawPaper")
            {
                if (Physics.Raycast(joyStickPointer.transform.localPosition, transform.forward, out hit, MaxDistance, LayerMask))
                    eraserPos = hit.point;

                thisEraser = (GameObject)Instantiate(eraserPrefab,
                    eraserPos, Quaternion.identity);
            }
        }
    }
}
