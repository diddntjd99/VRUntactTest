using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeScript : MonoBehaviour
{
    public GameObject startCutPos;
    public GameObject endCutPos;
    public GameObject joyStickPointer;
    
    public GameObject originalCone;
    public GameObject leftDividedCone;
    public GameObject rightDiviedeCone;
    public GameObject dottedLine;

    public JoyStick_Pointer_Script joyStick_Pointer_Script;

    public bool isCutCone = false;
    bool movePointerToStart = false;

    float joyStickPointerSpeed = 60f;

    public QmoveButtonScript qmoveButtonScript;
    GameObject pointer_hit_GameObject;
    bool isRotating = false;
    public float spinForce = 100f;
    bool canRotateHorizontally;
    bool canRotateVertically;

    bool isOutlined;

    // Start is called before the first frame update
    void Start()
    {
        isOutlined = false;
        DisableOutline();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && dottedLine.activeSelf == true)
        {
            isCutCone = true;
        }

        if (isCutCone && movePointerToStart == false)
        {
            MovePointerToStartCutPos();
        }
        if (movePointerToStart)
        {
            MovePointer();
        }

        if(rightDiviedeCone.activeSelf == true)
        {
            //이동
            // 조이스틱 포인터 사용불가일 때 (q보드 위에 있을 때) 아래의 코드를 실행하지 않는다.
            if (!qmoveButtonScript.ReturnCanUseJoyStickPointer())
            {
                return;
            }

            //큐브에 커서
            if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Cube"))
            {
                pointer_hit_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject.transform.root.gameObject;

                //클릭하지 않은 상태: 아웃라인 들어옴
                if (!isOutlined && !isRotating)
                {
                    EnableOutline();
                }

                //큐브를 클릭
                if (Input.GetButtonDown("Fire1"))
                {
                    isRotating = true;
                    joyStickPointer.SetActive(false);
                    DisableOutline();
                }
            }
            //큐브에 커서 X
            else
            {
                //커서가 벗어나기 직전의 큐브의 아웃라인 꺼주기
                if (pointer_hit_GameObject && isOutlined)
                {
                    DisableOutline();
                }
                pointer_hit_GameObject = null;
            }

            if (isRotating)
            {
                if (Input.GetButton("Fire1"))
                {
                    //대상 이름에 따라 돌릴 수 있는 방향 설정
                    if (pointer_hit_GameObject != null)
                    {
                        switch (pointer_hit_GameObject.name)
                        {
                            case "Shape1":
                                canRotateHorizontally = false;
                                canRotateVertically = true;
                                break;
                            case "RightCone":
                                canRotateHorizontally = true;
                                canRotateVertically = false;
                                break;
                            default:
                                canRotateHorizontally = true;
                                canRotateVertically = true;
                                break;

                        }
                        //요리조리 돌린다
                        float h = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                        float v = Input.GetAxisRaw("Vertical") * Time.deltaTime;
                        if (canRotateHorizontally)
                        {
                            pointer_hit_GameObject.transform.Rotate(0.0f, -h * spinForce, 0.0f, Space.World);
                        }
                        if (canRotateVertically)
                        {
                            pointer_hit_GameObject.transform.Rotate(v * spinForce, 0.0f, 0.0f, Space.World);
                        }
                    }
                }
                else
                {
                    isRotating = false;
                    joyStickPointer.SetActive(true);
                }
            }
        }


    }

    void MovePointerToStartCutPos()
    {
        joyStickPointer.transform.position = new Vector3(startCutPos.transform.position.x, startCutPos.transform.position.y, joyStickPointer.transform.position.z);
        movePointerToStart = true;
    }

    void MovePointer()
    {
        joyStick_Pointer_Script.moveSpeed = 0f;
        joyStickPointer.transform.Translate(new Vector3(0f, 0f, 1f * joyStickPointerSpeed * Time.deltaTime));

        if(joyStickPointer.transform.position.y <= endCutPos.transform.position.y)
        {
            joyStickPointer.transform.position = new Vector3(endCutPos.transform.position.x, endCutPos.transform.position.y, joyStickPointer.transform.position.z);
            movePointerToStart = false;
            isCutCone = false;
            joyStick_Pointer_Script.moveSpeed = 150f;

            // 자르는 포인터 이동 완료, 물체 두동강 내기
            originalCone.SetActive(false);
            rightDiviedeCone.SetActive(true);
            dottedLine.SetActive(false);
        }
    }
    void EnableOutline()
    {
        rightDiviedeCone.GetComponent<cakeslice.Outline>().enabled = true;
        isOutlined = true;
    }
    void DisableOutline()
    {
        rightDiviedeCone.gameObject.GetComponent<cakeslice.Outline>().enabled = false;
        isOutlined = false;
    }
}
