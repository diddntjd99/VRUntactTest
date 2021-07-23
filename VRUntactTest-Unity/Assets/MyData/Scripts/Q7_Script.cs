using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q7_Script : MonoBehaviour
{
    public Toggle q7_Toggle1; // magenta
    public Toggle q7_Toggle2; // cyan
    public Toggle q7_Toggle3; // white
    public Toggle q7_Toggle4; // yellow

    public GameObject white;
    public GameObject magenta;
    public GameObject cyan;
    public GameObject yellow;

    Color[] paintColorArr = new Color[4];
    Color currentPaintColor = Color.yellow; // 디폴트

    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    GameObject ray_Hitted_GameObject;
    GameObject colorChanged_GameObject = null;

    private void Awake()
    {
        // 토글 변화 시 실행
        q7_Toggle1.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[0];
                Debug.Log(currentPaintColor);
            }
        });

        q7_Toggle2.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[1];
                Debug.Log(currentPaintColor);
            }
        });

        q7_Toggle3.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[2];
                Debug.Log(currentPaintColor);
            }
        });

        q7_Toggle4.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                currentPaintColor = paintColorArr[3];
                Debug.Log(currentPaintColor);
            }
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        paintColorArr[0] = Color.magenta;
        paintColorArr[1] = Color.cyan;
        paintColorArr[2] = Color.white;
        paintColorArr[3] = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if(joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Paint"))
        {
            ray_Hitted_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject;
            //Debug.Log(ray_Hitted_GameObject);
            // 위의 토글과 밑의 도형으로 나뉘어야함
            switch (ray_Hitted_GameObject.name)
            {
                // 포인터가 토글에 갔을 때 처리
                case "Q7_Toggle1":
                case "Q7_Toggle2":
                case "Q7_Toggle3":
                case "Q7_Toggle4":
                    ray_Hitted_GameObject.transform.Find("Background").GetComponent<Image>().color = Color.red;
                    colorChanged_GameObject = ray_Hitted_GameObject;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // 마우스 클릭 시 토글 껐다 켰다함
                        ray_Hitted_GameObject.GetComponent<Toggle>().isOn = true;
                    }
                    break;
                case "White":
                case "Magenta":
                case "Cyan":
                case "Yellow":
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // 마우스 클릭 시 선택한 페인트 색으로 선택한 도형을 칠함.
                        ray_Hitted_GameObject.GetComponent<MeshRenderer>().material.color = currentPaintColor;
                    }
                    break;
            }
        }
        else
        {
            ray_Hitted_GameObject = null;
            if(colorChanged_GameObject != null)
            {
                colorChanged_GameObject.transform.Find("Background").GetComponent<Image>().color = Color.white;
                colorChanged_GameObject = null;
            }
        }
    }

    public bool CheckAnswer()
    {
        if(white.GetComponent<MeshRenderer>().material.color == Color.white &&
            magenta.GetComponent<MeshRenderer>().material.color == Color.magenta &&
            cyan.GetComponent<MeshRenderer>().material.color == Color.cyan &&
            yellow.GetComponent<MeshRenderer>().material.color == Color.yellow)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
