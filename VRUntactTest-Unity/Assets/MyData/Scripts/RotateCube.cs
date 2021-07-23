using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스크립트를 넣을 적당할 곳을 모르겠어서 일단 SceneManager에 넣음.
// 항상 실행되는 곳에 넣어주면 되겠습니다.

namespace cakeslice
{
    public class RotateCube : MonoBehaviour
    {
        /*public*/
        JoyStick_Pointer_Script joyStick_Pointer_Script; //이거왜 인k펙터로 드래그 안되지?
        public QmoveButtonScript qmoveButtonScript;
        public GameObject joyStick_Pointer;
        GameObject pointer_hit_GameObject;
        public GameObject shape;
        bool isRotating;
        public float spinForce;
        bool canRotateHorizontally;
        bool canRotateVertically;

        Transform[] children;
        bool isOutlined;

        void Start()
        {
            isRotating = false;
            joyStick_Pointer_Script = joyStick_Pointer.GetComponent<JoyStick_Pointer_Script>();

            children = shape.GetComponentsInChildren<Transform>();
            isOutlined = false;
            DisableOutline();
        }

        void Update()
        {
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
                    joyStick_Pointer.SetActive(false);
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
                    joyStick_Pointer.SetActive(true);
                }
            }
        }

        void EnableOutline()
        {
            for (int i = 1; i < children.Length; i++)
            {
                children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = true;
            }
            isOutlined = true;
        }
        void DisableOutline()
        {
            for (int i = 1; i < children.Length; i++)
            {
                children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = false;
            }
            isOutlined = false;
        }
    }
}
