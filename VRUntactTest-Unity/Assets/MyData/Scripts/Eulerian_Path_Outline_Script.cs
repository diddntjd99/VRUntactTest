using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eulerian_Path_Outline_Script : MonoBehaviour
{
    public Transform originTransform;
    public Transform destTransform;
    LineRenderer lineRenderer;  

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, originTransform.position);
        lineRenderer.SetPosition(1, destTransform.position);
    }
}
