using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class Q9_Keyboard_Control : MonoBehaviour
    {
        public GameObject vr_Keyboard;
        public GameObject q9_CutCone;
        public ConeScript coneScript;
        public JoyStick_Pointer_Script joyStick_Pointer_Script;
        public GameObject q9_Question_Icon;
        public GameObject q9_Keyboard_Icon;

        // Start is called before the first frame update
        void Start()
        {
            q9_Question_Icon.GetComponent<cakeslice.Outline>().enabled = false;
            q9_Keyboard_Icon.GetComponent<cakeslice.Outline>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            // 자르는 중이면 실행안함
            if (coneScript.isCutCone == true)
            {
                return;
            }
            if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.name == "Q9_Question_Icon")
            {
                q9_Question_Icon.GetComponent<cakeslice.Outline>().enabled = true;
                q9_Keyboard_Icon.GetComponent<cakeslice.Outline>().enabled = false;
                if (Input.GetButtonDown("Fire1"))
                {
                    vr_Keyboard.SetActive(false);
                    q9_CutCone.SetActive(true);
                }
            }
            else if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.name == "Q9_Keyboard_Icon")
            {
                q9_Question_Icon.GetComponent<cakeslice.Outline>().enabled = false;
                q9_Keyboard_Icon.GetComponent<cakeslice.Outline>().enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    vr_Keyboard.SetActive(true);
                    q9_CutCone.SetActive(false);
                }
            }
            else
            {
                q9_Question_Icon.GetComponent<cakeslice.Outline>().enabled = false;
                q9_Keyboard_Icon.GetComponent<cakeslice.Outline>().enabled = false;
            }
        }
    }
}
