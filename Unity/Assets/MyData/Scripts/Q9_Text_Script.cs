using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q9_Text_Script : VR_Keyboard_Script
{
    //public VR_Keyboard_Script vr_Keyboard_Script;
    public string q9_Answer;

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
        q9_Answer = base.answer_Word;
    }
}
