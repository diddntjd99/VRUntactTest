using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;
    public List<Vector3> finger3dPositions;

    public GameObject joyStickPointer;
    public GameObject drawPaper;
    public GameObject myDrawing;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    public GameObject selectedGameObject;

    public GameObject trash;
    public GameObject pencil;
    public GameObject eraser;
    
    Vector3 startPos;
    RaycastHit hit;
    float MaxDistance = 1000.0f;
    public LayerMask LayerMask;
    public bool isLineBeingCreated = false;

    public List<GameObject> lineList;

    Transform[] trashChildren;
    Transform[] pencilChildren;
    Transform[] eraserChildren;
    Transform[] selectedGOChildren;
    bool isOutlined;

    // Start is called before the first frame update
    void Start()
    {
        trashChildren = trash.GetComponentsInChildren<Transform>();
        pencilChildren = pencil.GetComponentsInChildren<Transform>();
        eraserChildren = eraser.GetComponentsInChildren<Transform>();
        isOutlined = false;
        DisableOutline(trashChildren);
        DisableOutline(pencilChildren);
        DisableOutline(eraserChildren);

    }

    // Update is called once per frame
    void Update()
    {
        if(selectedGameObject != null)
        {
            selectedGOChildren = selectedGameObject.GetComponentsInChildren<Transform>();
        }

        // 조이스틱 포인터가 살아있을 때만 작동하도록
        if (joyStickPointer.activeSelf == true)
        {
            if(joyStick_Pointer_Script.ray_hitted_GameObject != null)
            {
                switch (joyStick_Pointer_Script.ray_hitted_GameObject.name)
                {
                    case "trash":
                        // 현재 선택되어있는거도 켜주고 , trash 도 켜주고
                        EnableOutline(trashChildren);
                        DisableOutline(pencilChildren);
                        DisableOutline(eraserChildren);
                        if(selectedGOChildren != null)
                        {
                            EnableOutline(selectedGOChildren);
                        }
                        break;
                    case "pencil":
                        DisableOutline(trashChildren);
                        EnableOutline(pencilChildren);
                        DisableOutline(eraserChildren);
                        if (selectedGOChildren != null)
                        {
                            EnableOutline(selectedGOChildren);
                        }
                        break;
                    case "eraser":
                        DisableOutline(trashChildren);
                        DisableOutline(pencilChildren);
                        EnableOutline(eraserChildren);
                        if (selectedGOChildren != null)
                        {
                            EnableOutline(selectedGOChildren);
                        }
                        break;
                    default:
                        //꺼주기
                        DisableOutline(trashChildren);
                        DisableOutline(pencilChildren);
                        DisableOutline(eraserChildren);
                        if (selectedGOChildren != null)
                        {
                            EnableOutline(selectedGOChildren);
                        }
                        break;
                }
            }
            else
            {
                //현재 선택되어있는거 켜주고, 나머지 꺼주고 selectedGameObject
                //꺼주기
                DisableOutline(trashChildren);
                DisableOutline(pencilChildren);
                DisableOutline(eraserChildren);
                if (selectedGOChildren != null)
                {
                    EnableOutline(selectedGOChildren);
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if(joyStick_Pointer_Script.ray_hitted_GameObject != null)
                {
                    Debug.Log(joyStick_Pointer_Script.ray_hitted_GameObject.name);

                    switch (joyStick_Pointer_Script.ray_hitted_GameObject.name)
                    {
                        case "trash":
                            // myDrawing에 저장된 자식들(내가 그린 라인들) 모두 삭제
                            for (int i = 0; i < myDrawing.transform.childCount; i++) 
                            { 
                                Destroy(myDrawing.transform.GetChild(i).gameObject); 
                            }
                            myDrawing.transform.DetachChildren();

                            selectedGameObject = joyStick_Pointer_Script.ray_hitted_GameObject;
                            //// 아웃라인 켜주기
                            //EnableOutline(trashChildren);
                            //DisableOutline(pencilChildren);
                            //DisableOutline(eraserChildren);
                            break;
                        case "pencil":
                            // 아웃라인 켜주기
                            selectedGameObject = joyStick_Pointer_Script.ray_hitted_GameObject;
                            //DisableOutline(trashChildren);
                            //EnableOutline(pencilChildren);
                            //DisableOutline(eraserChildren);
                            break;
                        case "eraser":
                            // 아웃라인 켜주기
                            selectedGameObject = joyStick_Pointer_Script.ray_hitted_GameObject;
                            //DisableOutline(trashChildren);
                            //DisableOutline(pencilChildren);
                            //EnableOutline(eraserChildren);
                            break;
                    }
                }

                // 연필 선택한 상태일 때만 그리기 가능
                if (selectedGameObject != null && selectedGameObject.name == "pencil")
                {
                    isLineBeingCreated = true;
                    CreateLine();
                }
            }
            if (Input.GetButton("Fire1"))
            {
                if (Physics.Raycast(joyStickPointer.transform.localPosition, transform.forward, out hit, MaxDistance, LayerMask))
                {
                    if (selectedGameObject != null && selectedGameObject.name == "pencil")
                    {
                        startPos = hit.point;
                        //Vector2 tempFingerPos = new Vector2(startPos.x, startPos.y);
                        Vector3 tempFingerPos = new Vector3(startPos.x, startPos.y, startPos.z);
                        //if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
                        //{
                        //    UpdateLine(tempFingerPos);
                        //}
                        //if (Vector3.Distance(tempFingerPos, finger3dPositions[fingerPositions.Count - 1]) > .1f)
                        //{
                        //    UpdateLine(tempFingerPos);
                        //}
                        UpdateLine(tempFingerPos);
                    }
                    //startPos = hit.point;
                    //Vector2 tempFingerPos = new Vector2(startPos.x, startPos.y);
                    //Vector3 tempFingerPos = new Vector3(startPos.x, startPos.y, startPos.z);
                    //if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
                    //{
                    //    UpdateLine(tempFingerPos);
                    //}
                    //if (Vector3.Distance(tempFingerPos, finger3dPositions[fingerPositions.Count - 1]) > .1f)
                    //{
                    //    UpdateLine(tempFingerPos);
                    //}
                    //UpdateLine(tempFingerPos);
                }

            }
            if (Input.GetButtonUp("Fire1"))
            {
                isLineBeingCreated = false;
            }
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        
        //lineList.Add(currentLine);
        
        currentLine.transform.parent = myDrawing.transform;
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        finger3dPositions.Clear();
        if (Physics.Raycast(joyStickPointer.transform.localPosition, transform.forward, out hit, MaxDistance, LayerMask))
        {
            startPos = hit.point;
            fingerPositions.Add(new Vector2(startPos.x, startPos.y));
            fingerPositions.Add(new Vector2(startPos.x, startPos.y));
            //lineRenderer.SetPosition(0, fingerPositions[0]);
            //lineRenderer.SetPosition(1, fingerPositions[1]);
            finger3dPositions.Add(new Vector3(startPos.x, startPos.y, startPos.z));
            finger3dPositions.Add(new Vector3(startPos.x, startPos.y, startPos.z));
            lineRenderer.SetPosition(0, finger3dPositions[0]);
            lineRenderer.SetPosition(1, finger3dPositions[1]);
            edgeCollider.points = fingerPositions.ToArray();
        }
            
    }

    void UpdateLine(Vector3 newFingerPos)
    {
        Vector2 fingerpos = new Vector2(newFingerPos.x, newFingerPos.y);
        fingerPositions.Add(fingerpos);
        finger3dPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }


    void EnableOutline(Transform[] children)
    {
        for (int i = 2; i < children.Length; i++)
        {
            children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = true;
        }
        isOutlined = true;
    }
    void DisableOutline(Transform[] children)
    {
        for (int i = 2; i < children.Length; i++)
        {
            children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = false;
        }
        isOutlined = false;
    }
}
