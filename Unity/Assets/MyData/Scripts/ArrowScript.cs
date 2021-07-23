using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    /*public*/ JoyStick_Pointer_Script joyStick_Pointer_Script; //이거왜 인스펙터로 드래그 안되지?
    public GameObject joyStick_Pointer;
    GameObject pointer_hit_GameObject;
    public Material originalMaterial;
    public Material changedMaterial;
    public SceneManagement sceneManagement;

    void Start()
    {
        joyStick_Pointer_Script = joyStick_Pointer.GetComponent<JoyStick_Pointer_Script>();
    }

    void Update()
    {
        //화살표에 커서
        if (joyStick_Pointer.activeSelf == true && joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            pointer_hit_GameObject = joyStick_Pointer_Script.ray_hitted_GameObject.transform.parent.gameObject;

            if (pointer_hit_GameObject.GetComponent<MeshRenderer>())
            {
                pointer_hit_GameObject.transform.GetComponent<MeshRenderer>().material = changedMaterial;
            }

            // 화살표 클릭시 문제 변환
            if (Input.GetButtonDown("Fire1"))
            {
                if (pointer_hit_GameObject.transform.gameObject.name == "Arrow_Left" && this.gameObject.name == "Arrow_Left")
                {
                    sceneManagement.current_QustionNumber -= 1;
                }
                else if (pointer_hit_GameObject.transform.gameObject.name == "Arrow_Right" && this.gameObject.name == "Arrow_Right")
                {
                    sceneManagement.current_QustionNumber += 1;
                }
                Debug.Log(sceneManagement.current_QustionNumber);
            }
            
        }
        else
        {
            if (pointer_hit_GameObject != null)
            {
                if (pointer_hit_GameObject.GetComponent<MeshRenderer>())
                {
                    pointer_hit_GameObject.GetComponent<MeshRenderer>().material = originalMaterial;
                }
            }
        }
    }
}
