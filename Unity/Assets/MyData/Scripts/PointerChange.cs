using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerChange : MonoBehaviour
{
    public GameObject drawLineManager;
    public GameObject joyStickPointer;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;

    public DrawLine drawLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(drawLineManager.activeSelf == true)
        {
            // 켜져있을때 (즉, 문제 3번일때)
            // 선택한 커서로 포인터 체인지
            if (joyStick_Pointer_Script.ray_hitted_GameObject!= null && (joyStick_Pointer_Script.ray_hitted_GameObject.name == "DrawPaper"))
            {
                if(drawLine.selectedGameObject != null)
                {
                    switch (drawLine.selectedGameObject.name)
                    {
                        case "trash":
                            // 자식 오브젝트 finger
                            joyStickPointer.transform.GetChild(0).gameObject.SetActive(true);
                            // 자식 오브젝트 eraser
                            joyStickPointer.transform.GetChild(1).gameObject.SetActive(false);
                            // 자식 오브젝트 pencil
                            joyStickPointer.transform.GetChild(2).gameObject.SetActive(false);
                            break;
                        case "eraser":
                            // 자식 오브젝트 finger
                            joyStickPointer.transform.GetChild(0).gameObject.SetActive(false);
                            // 자식 오브젝트 eraser
                            joyStickPointer.transform.GetChild(1).gameObject.SetActive(true);
                            // 자식 오브젝트 pencil
                            joyStickPointer.transform.GetChild(2).gameObject.SetActive(false);
                            break;
                        case "pencil":
                            // 자식 오브젝트 finger
                            joyStickPointer.transform.GetChild(0).gameObject.SetActive(false);
                            // 자식 오브젝트 eraser
                            joyStickPointer.transform.GetChild(1).gameObject.SetActive(false);
                            // 자식 오브젝트 pencil
                            joyStickPointer.transform.GetChild(2).gameObject.SetActive(true);
                            break;
                    }
                }
            }
            else
            {
                // 자식 오브젝트 finger
                joyStickPointer.transform.GetChild(0).gameObject.SetActive(true);
                // 자식 오브젝트 eraser
                joyStickPointer.transform.GetChild(1).gameObject.SetActive(false);
                // 자식 오브젝트 pencil
                joyStickPointer.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        else
        {
            // 자식 오브젝트 finger
            joyStickPointer.transform.GetChild(0).gameObject.SetActive(true);
            // 자식 오브젝트 eraser
            joyStickPointer.transform.GetChild(1).gameObject.SetActive(false);
            // 자식 오브젝트 pencil
            joyStickPointer.transform.GetChild(2).gameObject.SetActive(false);
        }
        
    }
}
