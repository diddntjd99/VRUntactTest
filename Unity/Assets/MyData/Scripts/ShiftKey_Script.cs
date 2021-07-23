using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftKey_Script : MonoBehaviour
{
    public VR_Keyboard_Script vr_Keyboard_Script;
    public Material originalMaterial;
    public Material changedMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (vr_Keyboard_Script.isShiftPressed)
        {
            this.transform.Find("Key_Selected").gameObject.SetActive(true);
            this.transform.Find("Key_Selected").GetComponent<MeshRenderer>().material = changedMaterial;
        }
        else
        {
            this.transform.Find("Key_Selected").gameObject.SetActive(false);
            this.transform.Find("Key_Selected").GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }
}
