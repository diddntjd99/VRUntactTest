using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//현재 하이어라키에서 Q10_Shape를 언팩하면 동작하지 않는 문제가 있음. (프리팹 상태로 두세요)

//이 스크립트에서 결정한 각 꼭지점의 번호는 아래 이미지 링크에서 볼 수 있습니다.
//https://imgur.com/a/d9pKIFL

public class Eulerian_Path_Manager : MonoBehaviour
{
    public GameObject eulerian_path;
    public GameObject eulerian_outline;
    public GameObject refresh_button;
    public GameObject startMark;

    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    public QmoveButtonScript qmoveButtonScript;
    public GameObject joyStickPointer;
    GameObject ray_hit_gameObject;

    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public Transform point5;
    public Transform point6;
    public Transform point7;
    public Transform point8;
    public Transform point9;

    public Text solCheckText;
    public float rotSpeed;

    Quaternion frontRot;
    Quaternion leftRot;
    Quaternion rightRot;
    Quaternion backRot;
    Quaternion upRot;
    Quaternion downRot;
    Quaternion destRot;

    bool[,] relation;
    bool[,] initialRelation;
    bool shouldSolutionCheck = false;
    bool shouldRotate = false;
    bool isSolvingMode = false;
    bool isSolved = false;

    public enum Status
    {
        point0, point1, point2, point3, point4, point5, point6, point7, point8, point9, movingOn
    }
    public Status status;

    public enum Side
    {
        left, right, up, down, noSide
    }

    void Start()
    {
        //윤곽선 설정
        InstantiateOutlines();

        // 회전 상태 6가지 설정 (시작 방향: point1이 가운데)
        frontRot = transform.rotation;
        destRot = frontRot;
        transform.Rotate(0, 90f, 0);
        rightRot = transform.rotation;
        transform.Rotate(0, 90f, 0);
        backRot = transform.rotation;
        transform.Rotate(0, 90f, 0);
        leftRot = transform.rotation;
        transform.Rotate(0, 90f, 0);
        transform.Rotate(-70f, 0, 0, Space.World);
        upRot = transform.rotation;
        transform.Rotate(180f, 0, 0, Space.World);
        downRot = transform.rotation;
        transform.Rotate(-110f, 0, 0, Space.World);

        // 시작점 설정 (기본: point1)
        status = Status.point1;

        // 소문자 l과 대문자 O를 이용해 1과 0처럼 보이게 함.
        // true, false로 나타내면 가독성이 떨어지고 그렇다고 c#에서 1이 true로 인식도 안되니 이렇게 함.
        bool l = true;
        bool O = false;

        // 점 0 ~ 9 사이의 관계 2차원 배열. l: 이동 가능, O: 이동 불가
        
        //                        Point 0  1  2  3  4  5  6  7  8  9        Point
        relation = new bool[10, 10] { { O, l, l, l, l, O, O, O, O, O },   // 0
                                      { l, O, l, O, l, l, O, O, O, O },   // 1
                                      { l, l, O, l, O, O, l, O, O, O },   // 2
                                      { l, O, l, O, l, O, O, l, O, O },   // 3
                                      { l, l, O, l, O, O, O, O, l, O },   // 4
                                      { O, l, O, O, O, O, l, O, l, l },   // 5
                                      { O, O, l, O, O, l, O, l, O, l },   // 6
                                      { O, O, O, l, O, O, l, O, l, l },   // 7
                                      { O, O, O, O, l, l, O, l, O, l },   // 8
                                      { O, O, O, O, O, l, l, l, l, O } }; // 9
        // 다시하기 용도 초기값 저장 (배열: call by ref)
        initialRelation = (bool[,])relation.Clone();
    }

    void Update()
    {
        // 조이스틱 포인터 사용불가일 때 (q보드 위에 있을 때) 아래의 코드를 실행하지 않는다.
        if (!qmoveButtonScript.ReturnCanUseJoyStickPointer())
        {
            return;
        }

        if (joyStick_Pointer_Script.ray_hitted_GameObject != null && (joyStick_Pointer_Script.ray_hitted_GameObject.name == "Collider1" ||
            joyStick_Pointer_Script.ray_hitted_GameObject.name == "Collider2" || joyStick_Pointer_Script.ray_hitted_GameObject.name == "Collider3" ||
            joyStick_Pointer_Script.ray_hitted_GameObject.name == "Collider4"))
        {
            return;
        }

        // 모드 토글 설정
        if (isSolvingMode)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //한붓그리기 불가능
                isSolvingMode = false; 
                //조이스틱 포인터 켜기
                joyStickPointer.SetActive(true);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //한붓그리기 가능
                isSolvingMode = true;
                //조이스틱 포인터 끄기
                joyStickPointer.SetActive(false);
            }
        }

        // Solving Mode 시에만 동작
        if (isSolvingMode)
        {
            //입력
            if (status != Status.movingOn)
            {
                // 한붓그리기 컨트롤
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical")))
                {
                    // 오른쪽
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        InstantiateLine_And_RotateShape(Side.right);
                    }
                    //왼쪽
                    if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        InstantiateLine_And_RotateShape(Side.left);
                    }
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < Mathf.Abs(Input.GetAxisRaw("Vertical")))
                {
                    // 위쪽
                    if (Input.GetAxisRaw("Vertical") > 0)
                    {
                        InstantiateLine_And_RotateShape(Side.up);
                    }
                    // 아래쪽
                    if (Input.GetAxisRaw("Vertical") < 0)
                    {
                        InstantiateLine_And_RotateShape(Side.down);
                    }
                }
            }

            //회전
            if (shouldRotate)
            {
                //목적지에 맞게 도형도 회전
                transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, Time.deltaTime * rotSpeed * 150f);
                //도착하면 알아서 끝남
            }
        }

        //정답 판정: 모든 원소가 false여야 한다.
        if (shouldSolutionCheck)
        {
            bool tmp = false;
            for (int i = 0; i < relation.GetLength(0); i++)
            {
                for (int j = 0; j < relation.GetLength(1); j++)
                {
                    if (relation[i, j] == true)
                    {
                        tmp = true;
                        break;
                    }
                }
                if (tmp == true)
                {
                    break;
                }
            }
            if(tmp == false)
            {
                isSolved = true;
                solCheckText.text = "solved!";
            }
            shouldSolutionCheck = false;
        }

        //다시 시작(refresh)
        Debug.Log(ray_hit_gameObject);
        ray_hit_gameObject = joyStick_Pointer_Script.ray_hitted_GameObject;
        if (ray_hit_gameObject == refresh_button && Input.GetButtonDown("Fire1"))
        {
            Refresh();
        }

    }

    void Refresh()
    {
        //초기화
        transform.eulerAngles = new Vector3(-10f, 45f, -10f);
        status = Status.point1;
        relation = initialRelation;
        initialRelation = (bool[,])relation.Clone();
        solCheckText.text = "Not Solved";
        destRot = frontRot;
        shouldSolutionCheck = false;
        shouldRotate = false; 
        isSolvingMode = false;
        isSolved = false;
        joyStickPointer.SetActive(true);
        startMark.SetActive(true);

        //그어진 선 삭제
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Eulerian_Path"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    void InstantiateOutlines()
    {
        //하드코딩 (이차원 배열이 바뀌면 이것도 바꿔줘야함)
        InstantiateOutline(0, 1);
        InstantiateOutline(0, 2);
        InstantiateOutline(0, 3);
        InstantiateOutline(0, 4);
        InstantiateOutline(1, 2);
        InstantiateOutline(1, 4);
        InstantiateOutline(1, 5);
        InstantiateOutline(2, 3);
        InstantiateOutline(2, 6);
        InstantiateOutline(3, 4);
        InstantiateOutline(3, 7);
        InstantiateOutline(4, 8);
        InstantiateOutline(5, 6);
        InstantiateOutline(5, 8);
        InstantiateOutline(5, 9);
        InstantiateOutline(6, 7);
        InstantiateOutline(6, 9);
        InstantiateOutline(7, 8);
        InstantiateOutline(7, 9);
        InstantiateOutline(8, 9);
    }

    void InstantiateOutline(int origin, int dest)
    {
        Transform originTransform = null;
        Transform destTransform = null;
        switch (origin)
        {
            case 0:
                originTransform = point0;
                break;
            case 1:
                originTransform = point1;
                break;
            case 2:
                originTransform = point2;
                break;
            case 3:
                originTransform = point3;
                break;
            case 4:
                originTransform = point4;
                break;
            case 5:
                originTransform = point5;
                break;
            case 6:
                originTransform = point6;
                break;
            case 7:
                originTransform = point7;
                break;
            case 8:
                originTransform = point8;
                break;
            case 9:
                originTransform = point9;
                break;
            default:
                Debug.Log("Something is Wrong!");
                break;
        }
        switch (dest)
        {
            case 0:
                destTransform = point0;
                break;
            case 1:
                destTransform = point1;
                break;
            case 2:
                destTransform = point2;
                break;
            case 3:
                destTransform = point3;
                break;
            case 4:
                destTransform = point4;
                break;
            case 5:
                destTransform = point5;
                break;
            case 6:
                destTransform = point6;
                break;
            case 7:
                destTransform = point7;
                break;
            case 8:
                destTransform = point8;
                break;
            case 9:
                destTransform = point9;
                break;
            default:
                Debug.Log("Something is Wrong!");
                break;
        }

        GameObject outline = Instantiate(eulerian_outline, this.transform);
        outline.SetActive(true);
        //굵기 및 setPosition 설정은 outline 개별의 스크립트에서 관리
        outline.GetComponent<Eulerian_Path_Outline_Script>().originTransform = originTransform;
        outline.GetComponent<Eulerian_Path_Outline_Script>().destTransform = destTransform;
    }

    void InstantiateLine_And_RotateShape(Side side)
    {
        int destNum = -1;
        switch ((int)status)
        {
            case 0:
                switch (side)
                {
                    case Side.left:
                        destNum = 4;
                        break;
                    case Side.right:
                        destNum = 2;
                        break;
                    case Side.up:
                        destNum = 3;
                        break;
                    case Side.down:
                        destNum = 1;
                        break;
                }
                break;
            case 1:
                switch (side)
                {
                    case Side.left:
                        destNum = 4;
                        break;
                    case Side.right:
                        destNum = 2;
                        break;
                    case Side.up:
                        destNum = 0;
                        break;
                    case Side.down:
                        destNum = 5;
                        break;
                }
                break;
            case 2:
                switch (side)
                {
                    case Side.left:
                        destNum = 1;
                        break;
                    case Side.right:
                        destNum = 3;
                        break;
                    case Side.up:
                        destNum = 0;
                        break;
                    case Side.down:
                        destNum = 6;
                        break;
                }
                break;
            case 3:
                switch (side)
                {
                    case Side.left:
                        destNum = 2;
                        break;
                    case Side.right:
                        destNum = 4;
                        break;
                    case Side.up:
                        destNum = 0;
                        break;
                    case Side.down:
                        destNum = 7;
                        break;
                }
                break;
            case 4:
                switch (side)
                {
                    case Side.left:
                        destNum = 3;
                        break;
                    case Side.right:
                        destNum = 1;
                        break;
                    case Side.up:
                        destNum = 0;
                        break;
                    case Side.down:
                        destNum = 8;
                        break;
                }
                break;
            case 5:
                switch (side)
                {
                    case Side.left:
                        destNum = 8;
                        break;
                    case Side.right:
                        destNum = 6;
                        break;
                    case Side.up:
                        destNum = 1;
                        break;
                    case Side.down:
                        destNum = 9;
                        break;
                }
                break;
            case 6:
                switch (side)
                {
                    case Side.left:
                        destNum = 5;
                        break;
                    case Side.right:
                        destNum = 7;
                        break;
                    case Side.up:
                        destNum = 2;
                        break;
                    case Side.down:
                        destNum = 9;
                        break;
                }
                break;
            case 7:
                switch (side)
                {
                    case Side.left:
                        destNum = 6;
                        break;
                    case Side.right:
                        destNum = 8;
                        break;
                    case Side.up:
                        destNum = 3;
                        break;
                    case Side.down:
                        destNum = 9;
                        break;
                }
                break;
            case 8:
                switch (side)
                {
                    case Side.left:
                        destNum = 7;
                        break;
                    case Side.right:
                        destNum = 5;
                        break;
                    case Side.up:
                        destNum = 4;
                        break;
                    case Side.down:
                        destNum = 9;
                        break;
                }
                break;
            case 9:
                switch (side)
                {
                    case Side.left:
                        destNum = 8;
                        break;
                    case Side.right:
                        destNum = 6;
                        break;
                    case Side.up:
                        destNum = 5;
                        break;
                    case Side.down:
                        destNum = 7;
                        break;
                }
                break;
            default:
                Debug.Log("Something is Wrong!");
                break;
        }

        //두 점 사이의 거리가 지나갈 수 있는 상태일 때
        if (relation[(int)status, destNum])
        {
            //해당 길은 지나감 처리
            relation[(int)status, destNum] = false;
            relation[destNum, (int)status] = false;

            GameObject line = Instantiate(eulerian_path, this.transform);
            line.SetActive(true);
            line.tag = "Eulerian_Path";
            Transform originTransform = null;
            Transform destTransform = null;
            switch ((int)status)
            {
                case 0:
                    originTransform = point0;
                    break;
                case 1:
                    originTransform = point1;
                    break;
                case 2:
                    originTransform = point2;
                    break;
                case 3:
                    originTransform = point3;
                    break;
                case 4:
                    originTransform = point4;
                    break;
                case 5:
                    originTransform = point5;
                    break;
                case 6:
                    originTransform = point6;
                    break;
                case 7:
                    originTransform = point7;
                    break;
                case 8:
                    originTransform = point8;
                    break;
                case 9:
                    originTransform = point9;
                    break;
                default:
                    Debug.Log("Something is Wrong!");
                    break;

            }
            switch (destNum)
            {
                case 0:
                    destTransform = point0;
                    destRot = upRot;
                    break;
                case 1:
                    destTransform = point1;
                    destRot = frontRot;
                    break;
                case 2:
                    destTransform = point2;
                    destRot = rightRot;
                    break;
                case 3:
                    destTransform = point3;
                    destRot = backRot;
                    break;
                case 4:
                    destTransform = point4;
                    destRot = leftRot;
                    break;
                case 5:
                    destTransform = point5;
                    destRot = frontRot;
                    break;
                case 6:
                    destTransform = point6;
                    destRot = rightRot;
                    break;
                case 7:
                    destTransform = point7;
                    destRot = backRot;
                    break;
                case 8:
                    destTransform = point8;
                    destRot = leftRot;
                    break;
                case 9:
                    destTransform = point9;
                    destRot = downRot;
                    break;
                default:
                    Debug.Log("Something is Wrong!");
                    break;
            }
            line.GetComponent<Eulerian_Path_Script>().origin = originTransform;
            line.GetComponent<Eulerian_Path_Script>().destination = destTransform;
            line.GetComponent<Eulerian_Path_Script>().destNum = destNum;

            //생성되는 동안 status는 movingOn
            status = Status.movingOn;
            //한붓그리기script에서 다 그어지면 status 목적지 선으로 설정됨.

            shouldSolutionCheck = true;
            shouldRotate = true;

            //시작 마크 지우기 (사실 맨 처음 이동시에만 지워주는 게 베스트)
            startMark.SetActive(false);
        }
    }

    public bool CheckAnswer()
    {
        return isSolved;
    }

}
