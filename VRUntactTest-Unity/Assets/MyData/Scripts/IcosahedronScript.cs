using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 상하좌우 클릭 후 컨트롤 가능
// 4색 채우기
public class IcosahedronScript : MonoBehaviour
{
    /*public*/
    JoyStick_Pointer_Script joyStick_Pointer_Script; //이거왜 인스펙터로 드래그 안되지?
    public QmoveButtonScript qmoveButtonScript;
    public GameObject joyStick_Pointer;
    GameObject pointer_hit_GameObject;
    bool isRotating;
    public float spinForce;
    bool canRotateHorizontally;
    bool canRotateVertically;

    public Toggle q8_Toggle1; // red
    public Toggle q8_Toggle2; // green
    public Toggle q8_Toggle3; // blue
    public Toggle q8_Toggle4; // yellow

    public GameObject red;
    public GameObject green;
    public GameObject blue;
    public GameObject yellow;

    Color[] paintColorArr = new Color[4];
    Color currentPaintColor = Color.yellow; // 디폴트

    GameObject ray_Hitted_GameObject;
    GameObject colorChanged_GameObject = null;

    Transform[] children;
    bool isOutlined;

    private void Awake()
    {
        // 토글 변화 시 실행
        q8_Toggle1.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[0];
                Debug.Log(currentPaintColor);
            }
        });

        q8_Toggle2.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[1];
                Debug.Log(currentPaintColor);
            }
        });

        q8_Toggle3.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[2];
                Debug.Log(currentPaintColor);
            }
        });

        q8_Toggle4.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[3];
                Debug.Log(currentPaintColor);
            }
        });
    }

    void Start()
    {
        paintColorArr[0] = Color.red;
        paintColorArr[1] = Color.green;
        paintColorArr[2] = Color.blue;
        paintColorArr[3] = Color.yellow;

        isRotating = false;
        joyStick_Pointer_Script = joyStick_Pointer.GetComponent<JoyStick_Pointer_Script>();

        children = this.GetComponentsInChildren<Transform>();
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

        if(joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Paint"))
        {
            ray_Hitted_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject;

            switch (ray_Hitted_GameObject.name)
            {
                // 포인터가 토글에 갔을 때 처리
                case "Q8_Toggle1":
                case "Q8_Toggle2":
                case "Q8_Toggle3":
                case "Q8_Toggle4":
                    ray_Hitted_GameObject.transform.Find("Background").GetComponent<Image>().color = Color.red;
                    colorChanged_GameObject = ray_Hitted_GameObject;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // 마우스 클릭 시 토글 껐다 켰다함
                        ray_Hitted_GameObject.GetComponent<Toggle>().isOn = true;
                    }
                    break;
            }
        }
        else
        {
            ray_Hitted_GameObject = null;
            if (colorChanged_GameObject != null)
            {
                colorChanged_GameObject.transform.Find("Background").GetComponent<Image>().color = Color.white;
                colorChanged_GameObject = null;
            }
        }

        //큐브에 커서
        if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Cube")
           )
        {
            // 부모 오브젝트
            pointer_hit_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject.transform.root.gameObject;
            ray_Hitted_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject;

            //클릭하지 않은 상태: 아웃라인 들어옴
            if (!isOutlined && !isRotating)
            {
                //pointer_hit_GameObject.GetComponent<Outline>().enabled = true;
                EnableOutline();

                switch (ray_Hitted_GameObject.name)
                {
                    case "Green":
                    case "Blue":
                    case "Yellow":
                        if (Input.GetButtonDown("Fire1"))
                        {
                            // 마우스 클릭 시 선택한 페인트 색으로 선택한 도형을 칠함.
                            ray_Hitted_GameObject.GetComponent<MeshRenderer>().material.color = currentPaintColor;
                        }
                        break;
                }
            }


            //클릭하지 않은 상태: 아웃라인 들어옴
            if (!isRotating)
            {
                switch (ray_Hitted_GameObject.name)
                {
                    case "Green":
                    case "Blue":
                    case "Yellow":
                        if (Input.GetButtonDown("Fire1"))
                        {
                            // 마우스 클릭 시 선택한 페인트 색으로 선택한 도형을 칠함.
                            ray_Hitted_GameObject.GetComponent<MeshRenderer>().material.color = currentPaintColor;
                        }
                        break;
                }
            }

            //큐브를 우클릭
            if (Input.GetButton("Fire2"))
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
            if (Input.GetButton("Fire2"))
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

    public bool CheckAnswer()
    {
        if (green.GetComponent<MeshRenderer>().material.color == Color.green &&
            blue.GetComponent<MeshRenderer>().material.color == Color.blue &&
            yellow.GetComponent<MeshRenderer>().material.color == Color.yellow)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void EnableOutline()
    {
        for(int i = 6; i < children.Length; i++)
        {
            children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = true;
        }
        isOutlined = true;
    }
    void DisableOutline()
    {
        for (int i = 6; i < children.Length; i++)
        {
            children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = false;
        }
        isOutlined = false;
    }
}