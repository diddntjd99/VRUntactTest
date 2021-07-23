using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q1_Toggle_Script : MonoBehaviour
{
    public Toggle q1_tgl_1;
    public Toggle q1_tgl_2;
    public Toggle q1_tgl_3;
    public Toggle q1_tgl_4;

    public string q1_Answer = "4";

    public GameObject joyStickPointer;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    GameObject ray_hitted_GameObject;
    string q_name;
    GameObject colorChanged_GameObject = null;



    private void Awake()
    {
        q1_tgl_1.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                q1_Answer = "1";
                Debug.Log(q1_Answer);
            }
        });

        q1_tgl_2.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                q1_Answer = "2";
                Debug.Log(q1_Answer);
            }
        });

        q1_tgl_3.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                q1_Answer = "3";
                Debug.Log(q1_Answer);
            }
        });

        q1_tgl_4.onValueChanged.AddListener((bool on) =>
        {
            if (on)
            {
                q1_Answer = "4";
                Debug.Log(q1_Answer);
            }
        });
    }


    // Start is called before the first frame update
    void Start()
    {
        //joyStick_Pointer_Script = joyStickPointer.GetComponent<JoyStick_Pointer_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(q1_Answer);
        if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Answer_Toggle"))
        {
            ray_hitted_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject;
            q_name = ray_hitted_GameObject.name;
            //Debug.Log(q_name);

            switch (q_name)
            {
                case "Q1_Toggle1":
                case "Q1_Toggle2":
                case "Q1_Toggle3":
                case "Q1_Toggle4":
                    ray_hitted_GameObject.transform.Find("Background").GetComponent<Image>().color = Color.red;
                    colorChanged_GameObject = ray_hitted_GameObject;
                    break;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Select_Answer();
            }
        }
        else
        {
            ray_hitted_GameObject = null;
            if (colorChanged_GameObject != null)
            {
                colorChanged_GameObject.transform.Find("Background").GetComponent<Image>().color = Color.white;
                colorChanged_GameObject = null;
            }
        }
    }

    void Select_Answer()
    {
        if (ray_hitted_GameObject.layer == LayerMask.NameToLayer("Answer_Toggle"))
        {
            //Debug.Log(ray_hitted_GameObject);
            ray_hitted_GameObject.GetComponent<Toggle>().isOn = true;
        }
    }
}
