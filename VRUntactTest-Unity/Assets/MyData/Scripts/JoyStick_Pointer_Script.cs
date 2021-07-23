using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick_Pointer_Script : MonoBehaviour
{
    public float moveSpeed;
    public RaycastHit hit;
    public GameObject ray_hitted_GameObject = null;
    float MaxDistance = 1000.0f;
    
    void Start()
    {
        //포인터 시작 위치
    }

    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float v = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        //this.GetComponent<Rigidbody>().velocity
        //    = new Vector3(h * moveSpeed, v * moveSpeed, 0f);

        this.transform.position += new Vector3(h * moveSpeed, v * moveSpeed, 0f);

        if (Physics.Raycast(this.transform.localPosition, transform.up, out hit, MaxDistance))
        {
            ray_hitted_GameObject = hit.transform.gameObject;
            //Debug.Log(hit.transform.gameObject);
        }
        else
        {
            ray_hitted_GameObject = null;
        }

        Debug.DrawRay(this.transform.localPosition, transform.up * 1000, Color.blue, 0.5f);
    }
}
