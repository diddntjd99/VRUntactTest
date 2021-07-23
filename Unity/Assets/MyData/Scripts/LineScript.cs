using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    DrawLine drawline;
    Vector3 lineStartPos;
    Vector3 lineStart_MidPos;
    Vector3 lineMidPos;
    Vector3 lineMid_EndPos;
    Vector3 lineEndPos;
    public float min_Distance = 10f;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        drawline = GameObject.Find("DrawLineManager").GetComponent<DrawLine>();
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer.positionCount != 0 && drawline.isLineBeingCreated == false)
        {
            lineStartPos = lineRenderer.GetPosition(0);
            lineStart_MidPos = lineRenderer.GetPosition(lineRenderer.positionCount / 4);
            lineMidPos = lineRenderer.GetPosition(lineRenderer.positionCount / 2);
            lineMid_EndPos = lineRenderer.GetPosition(lineRenderer.positionCount * 3 / 4);
            lineEndPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

            if(Vector3.Distance(lineStartPos, lineEndPos) <= min_Distance  && Vector3.Distance(lineStartPos, lineStart_MidPos) <= min_Distance 
                && Vector3.Distance(lineStart_MidPos, lineMidPos) <= min_Distance && Vector3.Distance(lineStart_MidPos, lineEndPos) <= min_Distance)
            {
                //Debug.Log(lineStartPos);
                //Debug.Log(lineEndPos);
                Destroy(this.gameObject);
            }
        }
        
    }
}
