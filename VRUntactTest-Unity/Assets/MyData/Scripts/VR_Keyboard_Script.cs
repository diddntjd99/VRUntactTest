using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_Keyboard_Script : MonoBehaviour
{
    public GameObject vr_Keyboard;

    public GameObject joyStickPointer;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    float maxDistance = 1000.0f;
    RaycastHit hit;
    public LayerMask layerMask;
    GameObject ray_hitted_GameObject;
    GameObject colorChanged_GameObject = null;
    public string answer_Word = "";
    string keyName;
    string selectedKey;
    bool isBackSpacePressed = false;
    public bool isShiftPressed = false;
    bool shouldForceTurnOff = false;
    public bool isEnterPressed = false;
    GameObject forceTurningOff_GameObject = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        // 포인터가 특정 키를 가리킬때
        if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Keyboard") && shouldForceTurnOff == false)
        {
            ray_hitted_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject;
            GameObject obj = ray_hitted_GameObject.transform.Find("Key_Selected").gameObject;
            obj.SetActive(true);
            if (colorChanged_GameObject != null && colorChanged_GameObject != ray_hitted_GameObject)
            {
                shouldForceTurnOff = true;
                forceTurningOff_GameObject = colorChanged_GameObject;
            }
            colorChanged_GameObject = ray_hitted_GameObject;
            if (Input.GetButtonDown("Fire1"))
            {
                Select_Key();
            }
        }
        // 특정 키를 가리키지 않을 때
        else
        {
            //Debug.Log("colorChanged:" + colorChanged_GameObject);
            ray_hitted_GameObject = null;
            if (colorChanged_GameObject != null)
            {
                //Debug.Log("colorChanged:" + colorChanged_GameObject);
                colorChanged_GameObject.transform.Find("Key_Selected").gameObject.SetActive(false);
            }
            if (forceTurningOff_GameObject != null)
            {
                //Debug.Log("forceTurningOff:" + colorChanged_GameObject);
                forceTurningOff_GameObject.transform.Find("Key_Selected").gameObject.SetActive(false);
            }
            forceTurningOff_GameObject = null;
            shouldForceTurnOff = false;

        }
    }
    void Select_Key()
    {
        keyName = ray_hitted_GameObject.name;
        //Debug.Log(keyName + "키 입력됨");

        switch (keyName)
        {
            case "Text_a": selectedKey = "a"; break;
            case "Text_b": selectedKey = "b"; break;
            case "Text_c": selectedKey = "c"; break;
            case "Text_d": selectedKey = "d"; break;
            case "Text_e": selectedKey = "e"; break;
            case "Text_f": selectedKey = "f"; break;
            case "Text_g": selectedKey = "g"; break;
            case "Text_h": selectedKey = "h"; break;
            case "Text_i": selectedKey = "i"; break;
            case "Text_j": selectedKey = "j"; break;
            case "Text_k": selectedKey = "k"; break;
            case "Text_l": selectedKey = "l"; break;
            case "Text_m": selectedKey = "m"; break;
            case "Text_n": selectedKey = "n"; break;
            case "Text_o": selectedKey = "o"; break;
            case "Text_p": selectedKey = "p"; break;
            case "Text_q": selectedKey = "q"; break;
            case "Text_r": selectedKey = "r"; break;
            case "Text_s": selectedKey = "s"; break;
            case "Text_t": selectedKey = "t"; break;
            case "Text_u": selectedKey = "u"; break;
            case "Text_v": selectedKey = "v"; break;
            case "Text_w": selectedKey = "w"; break;
            case "Text_x": selectedKey = "x"; break;
            case "Text_y": selectedKey = "y"; break;
            case "Text_z": selectedKey = "z"; break;
            case "Text_0": selectedKey = "0"; break;
            case "Text_1": selectedKey = "1"; break;
            case "Text_2": selectedKey = "2"; break;
            case "Text_3": selectedKey = "3"; break;
            case "Text_4": selectedKey = "4"; break;
            case "Text_5": selectedKey = "5"; break;
            case "Text_6": selectedKey = "6"; break;
            case "Text_7": selectedKey = "7"; break;
            case "Text_8": selectedKey = "8"; break;
            case "Text_9": selectedKey = "9"; break;
            case "Text_,": selectedKey = ","; break;
            case "Text_.": selectedKey = "."; break;
            case "Text_'": selectedKey = "'"; break;
            case "Space": selectedKey = " "; break;
            case "BackSpace": selectedKey = ""; isBackSpacePressed = true; break;
            case "Enter": selectedKey = ""; /* 엔터 입력 시의 설정 필요! */isEnterPressed = true;  break;
            case "Shift": selectedKey = ""; isShiftPressed = !isShiftPressed; break;
            case "Delete": selectedKey = ""; answer_Word = string.Empty; break;
        }

        // shift키 눌렸을때
        if (isShiftPressed)
        {
            for (char i = 'a'; i <= 'z'; i++)
            {
                string objName = "Text_" + i.ToString();
                vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text.ToUpper();
                //Debug.Log("123");
            }
            for (char i = '0'; i <= '9'; i++)
            {
                string objName = "Text_" + i.ToString();
                switch (i)
                {
                    case '0':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = ")"; break;
                    case '1':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "!"; break;
                    case '2':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "@"; break;
                    case '3':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "#"; break;
                    case '4':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "$"; break;
                    case '5':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "%"; break;
                    case '6':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "^"; break;
                    case '7':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "&"; break;
                    case '8':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "*"; break;
                    case '9':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "("; break;
                }
            }
            //알파벳
            if (selectedKey != string.Empty && (('a' <= selectedKey[0] && selectedKey[0] <= 'z') || ('A' <= selectedKey[0] && selectedKey[0] <= 'Z')))
            {
                selectedKey = selectedKey.ToUpper();
            }
            //0~9
            else if (selectedKey != string.Empty && ('0' <= selectedKey[0] && selectedKey[0] <= '9'))
            {
                switch (selectedKey[0])
                {
                    case '0': selectedKey = ")"; break;
                    case '1': selectedKey = "!"; break;
                    case '2': selectedKey = "@"; break;
                    case '3': selectedKey = "#"; break;
                    case '4': selectedKey = "$"; break;
                    case '5': selectedKey = "%"; break;
                    case '6': selectedKey = "^"; break;
                    case '7': selectedKey = "&"; break;
                    case '8': selectedKey = "*"; break;
                    case '9': selectedKey = "("; break;
                }
            }
            //나머지(, . ')
            else { }
        }
        else
        {
            for (char i = 'a'; i <= 'z'; i++)
            {
                string objName = "Text_" + i.ToString();
                vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text.ToLower();
            }
            for (char i = '0'; i <= '9'; i++)
            {
                string objName = "Text_" + i.ToString();
                switch (i)
                {
                    case '0':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "0"; break;
                    case '1':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "1"; break;
                    case '2':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "2"; break;
                    case '3':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "3"; break;
                    case '4':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "4"; break;
                    case '5':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "5"; break;
                    case '6':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "6"; break;
                    case '7':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "7"; break;
                    case '8':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "8"; break;
                    case '9':
                        vr_Keyboard.transform.Find("Canvas").Find(objName).GetComponent<Text>().text = "9"; break;
                }
            }
        }


        // backspace 눌렸을 때 (아무것도 입력 안되어있으면 처리 안하도록 할것.)
        if (isBackSpacePressed)
        {
            if (answer_Word.Length >= 1)
            {
                answer_Word = answer_Word.Remove(answer_Word.Length - 1);
            }
            else
            {
                Debug.Log("지울 문자가 없음");
            }
            isBackSpacePressed = !isBackSpacePressed; // 문자 하나 지우고 false
        }
        // 아닐때
        else
        {
            answer_Word = answer_Word + selectedKey;
        }

        //Debug.Log(answer_Word);
        //Debug.Log(answer_Word.Length);
    }
}
