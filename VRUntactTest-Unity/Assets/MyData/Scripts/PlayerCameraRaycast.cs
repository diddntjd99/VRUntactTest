using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRaycast : MonoBehaviour
{
    public RaycastHit hit;
    public GameObject ray_hitted_GameObject = null;
    float MaxDistance = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 위치기준, z축은 카메라 기준으로 회전
        if (Physics.Raycast(this.transform.parent.localPosition, transform.forward, out hit, MaxDistance))
        {
            ray_hitted_GameObject = hit.transform.gameObject;
            //Debug.Log(hit.transform.gameObject);
        }
        else
        {
            ray_hitted_GameObject = null;
        }

        Debug.DrawRay(this.transform.parent.localPosition, transform.forward * 1000, Color.green, 0.5f);
    }
}
