using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginTextScript : VR_Keyboard_Script
{
    public string studentID;
    public GameObject SceneManager;
    public GameObject Login;
    public GameObject Login2;
    public GameObject Login_Text;
    public GameObject VR_Keyboard;
    public GameObject AnswerSend_Manager;
    public GameObject SEND_Text;
    public GameObject QmoveButton;
    public GameObject Cube;
    public GameObject TimeText;
    public GameObject TimeText2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == false)
        {
            return;
        }

        base.Update();
        this.GetComponent<Text>().text = base.answer_Word;
        studentID = base.answer_Word;

        if(base.isEnterPressed == true)
        {
            //엔터키 눌렀으니 학번입력 완료 후 시험시작.
            Login.SetActive(false);
            Login2.SetActive(false);
            Login_Text.SetActive(false);
            VR_Keyboard.SetActive(false);
            AnswerSend_Manager.SetActive(true);
            SEND_Text.SetActive(true);
            SceneManager.SetActive(true);
            QmoveButton.SetActive(true);
            Cube.SetActive(true);
            TimeText.SetActive(true);
            TimeText2.SetActive(true);
        }
    }
}
