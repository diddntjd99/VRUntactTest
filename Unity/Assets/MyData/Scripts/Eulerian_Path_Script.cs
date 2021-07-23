using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한붓그리기(Eulerian Path) 스크립트

public class Eulerian_Path_Script : MonoBehaviour
{
    public Eulerian_Path_Manager eulerian_path_manager;
    private LineRenderer lineRenderer;
    private float moveCounter;
    private float dist;
    private bool isDrawingFinished;
    public float lineDrawSpeed; // 설정: 도형의 자식 중 꺼져있는 라인의 인스펙터에서
    //[HideInInspector]
    public int destNum;
    //[HideInInspector]
    public Transform origin;
    //[HideInInspector]
    public Transform destination;
    Vector3 originPos;
    Vector3 destPos;
    Vector3 pointAlongLine;

    // SetPoisition에서 0은 시작점, 1은 출발점을 뜻함.
    // 이 게임오브젝트가 생성될 때 지정되는 변수: origin, destination, destNum

    void Start()
    {
        isDrawingFinished = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.startWidth = 8f;
        lineRenderer.endWidth = 8f;
        lineRenderer.useWorldSpace = true;
        moveCounter = 0f;
        dist = Vector3.Distance(origin.position, destination.position);

        originPos = origin.position;
        destPos = destination.position;
        pointAlongLine = originPos;

        lineRenderer.SetPosition(0, originPos);
        lineRenderer.SetPosition(1, pointAlongLine);
    }

    void Update()
    {
        originPos = origin.position;

        if (Mathf.Abs(Vector3.Distance(pointAlongLine, destination.position)) > 0.1f)
        {
            moveCounter += lineDrawSpeed * 0.1f * Time.deltaTime;
            float x = Mathf.Lerp(0, dist, moveCounter);
            destPos = destination.position;
            pointAlongLine = x * Vector3.Normalize(destPos - originPos) + originPos;

            lineRenderer.SetPosition(1, pointAlongLine);
        }
        else if (!isDrawingFinished)
        {
            eulerian_path_manager.status = (Eulerian_Path_Manager.Status)destNum;
            isDrawingFinished = true;
        }

        // 시작점은 parent가 회전한다고 해서 회전하지 않으니 고정시켜줘야함.
        lineRenderer.SetPosition(0, originPos);

    }
}
