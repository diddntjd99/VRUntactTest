using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HanoiTowerScript : MonoBehaviour
{
    Stack<int> base1Stack = new Stack<int>();
    Stack<int> base2Stack = new Stack<int>();
    Stack<int> base3Stack = new Stack<int>();

    public GameObject smallRing;
    public GameObject middleRing;
    public GameObject largeRing;
    public GameObject selectedRing;

    public GameObject joyStickPointer;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;

    public Text moveCountText;

    public QmoveButtonScript qmoveButtonScript;

    public bool canSelectRing = true; // 선택후 움직일때 false, 링을 놓았을때 true
    public bool canMoveRing = false; // 선택 후 true, 링을 놓았을때 false
    bool canMove;

    int pre_selected_BaseNumber;
    int selected_BaseNumber; // 전역변수는 0으로 초기화
    int max_BaseNumber = 3;
    int passPutRingFuncCount = 0;
    int selectedRingSize;

    // Large Ring
    int largeRingSize = 3;
    bool canSelectLargeRing = false;
    int largeRingCurrentBase = 1;

    // Middle Ring
    int middleRingSize = 2;
    bool canSelectMiddleRing = false;
    int middleRingCurrentBase = 1;

    // Small Ring
    int smallRingSize = 1;
    bool canSelectSmallRing = false;
    int smallRingCurrentBase = 1;

    int tryCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        base1Stack.Push(largeRingSize); // 3
        base1Stack.Push(middleRingSize); // 2
        base1Stack.Push(smallRingSize); // 1
    }

    // Update is called once per frame
    void Update()
    {
        if (qmoveButtonScript.ReturnCanUseJoyStickPointer())
        {
            if (canSelectRing)
            {
                CheckTopRings();
                SelectRing();
            }
            if (canMoveRing)
            {
                MoveRing(selectedRing);
                PutRing(selectedRing);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Refresh();
            }

            moveCountText.text = "Move Count:  " + tryCount;
        }
    }
    int ReturnTopRingSize(Stack<int> baseStack)
    {
        // 스택 비었을때
        if (baseStack.Count == 0)
        {
            return 0;
        }
        // 스택 차 있을때
        else
        {
            return(baseStack.Peek());
        }
    }

    void CheckTopRings()
    {
        canSelectLargeRing = false;
        canSelectMiddleRing = false;
        canSelectSmallRing = false;

        switch (ReturnTopRingSize(base1Stack))
        {
            case 0:
                break;
            case 1:
                canSelectSmallRing = true;
                break;
            case 2:
                canSelectMiddleRing = true;
                break;
            case 3:
                canSelectLargeRing = true;
                break;
        }
        switch (ReturnTopRingSize(base2Stack))
        {
            case 0:
                break;
            case 1:
                canSelectSmallRing = true;
                break;
            case 2:
                canSelectMiddleRing = true;
                break;
            case 3:
                canSelectLargeRing = true;
                break;
        }
        switch (ReturnTopRingSize(base3Stack))
        {
            case 0:
                break;
            case 1:
                canSelectSmallRing = true;
                break;
            case 2:
                canSelectMiddleRing = true;
                break;
            case 3:
                canSelectLargeRing = true;
                break;
        }
    }

    void SelectRing()
    {
        // 조이스틱 포인터를 쓸 수 없으면 ring선택부터 이동까지 모두 막는다.
        if (!qmoveButtonScript.ReturnCanUseJoyStickPointer())
        {
            //Debug.Log("123");
            return;
        }

        if(joyStickPointer.activeSelf == false)
        {
            joyStickPointer.SetActive(true);
        }

        // 포인터가 ring을 가리킬 때 실행
        if(joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("Ring"))
        {
            switch (joyStick_Pointer_Script.ray_hitted_GameObject.name)
            {
                case "LargeRing":
                    ProcessSelection(canSelectLargeRing, largeRing, middleRing, smallRing, largeRingCurrentBase, largeRingSize);
                    break;
                case "MiddleRing":
                    ProcessSelection(canSelectMiddleRing, middleRing, largeRing, smallRing, middleRingCurrentBase, middleRingSize);
                    break;
                case "SmallRing":
                    ProcessSelection(canSelectSmallRing, smallRing, largeRing, middleRing, smallRingCurrentBase, smallRingSize);
                    break;
            }
        }
        // 포인터가 다른데 가리키면 모두 흰색으로 
        else
        {
            largeRing.GetComponent<MeshRenderer>().material.color = Color.white;
            middleRing.GetComponent<MeshRenderer>().material.color = Color.white;
            smallRing.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    // sRing : 포인터가 가리키는 ring(색상 빨갛게 변경), ring1, ring2는 선택 안됐으므로 하얀색
    void ProcessSelection(bool canSelectRing, GameObject sRing, GameObject ring1, GameObject ring2, int sBaseNum, int sRingSize)
    {
        sRing.GetComponent<MeshRenderer>().material.color = Color.red;
        ring1.GetComponent<MeshRenderer>().material.color = Color.white;
        ring2.GetComponent<MeshRenderer>().material.color = Color.white;

        if (canSelectRing && Input.GetButtonDown("Fire1"))
        {
            selectedRing = sRing;
            selected_BaseNumber = sBaseNum;
            pre_selected_BaseNumber = sBaseNum;
            selectedRingSize = sRingSize;
            PopFromStack(selected_BaseNumber);
            selectedRing.transform.position = this.transform.Find("base" + selected_BaseNumber).Find("SelectedRingPos_" + selected_BaseNumber).position;
            canSelectRing = false;
            canMoveRing = true;
            passPutRingFuncCount = 0;
        }
    }

    void PopFromStack(int sBaseNum) // sBaseNum 은 selected_BaseNumber
    {
        switch (sBaseNum)
        {
            case 1:
                base1Stack.Pop();
                break;
            case 2:
                base2Stack.Pop();
                break;
            case 3:
                base3Stack.Pop();
                break;
        }
    }

    void MoveRing(GameObject sRing) // sRing은 selectedRing
    {
        // 포인터가 켜져있으면 끈다.
        if (joyStickPointer.activeSelf)
        {
            joyStickPointer.SetActive(false);
        }

        float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        //Debug.Log("h현재값: " + h);

        if (h > 0 && canMove)
        {
            //딱한번만 오른쪽으로 이동
            //다음 base의 위치로 Ring을 이동 (포지션 변경)
            if (selected_BaseNumber < max_BaseNumber)
            {
                selected_BaseNumber += 1;
            }

            sRing.transform.position = this.transform.Find("base" + selected_BaseNumber).Find("SelectedRingPos_" + selected_BaseNumber).position;
            
            canMove = false;
            //Debug.Log("키보드:우측키, 상태: "+ canMove);
        }
        else if (h < 0 && canMove)
        {
            //딱한번만 왼쪽으로 이동
            if (selected_BaseNumber > 1)
            {
                selected_BaseNumber -= 1;
            }

            sRing.transform.position = this.transform.Find("base" + selected_BaseNumber).Find("SelectedRingPos_" + selected_BaseNumber).position;

            canMove = false;
            //Debug.Log("키보드:좌측키, 상태: " + canMove);
        }
        else if (h == 0)
        {
            canMove = true;
        }
    }

    void PutRing(GameObject sRing)
    {
        // 도형을 클릭하여 선택하고 바로 실행되어 도형이 그자리에 놓여지는것을 막기 위함.
        if(passPutRingFuncCount == 0)
        {
            passPutRingFuncCount++;
            return;
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                switch (selected_BaseNumber)
                {
                    case 1:
                        ProcessPutRing(base1Stack);
                        break;
                    case 2:
                        ProcessPutRing(base2Stack);
                        break;
                    case 3:
                        ProcessPutRing(base3Stack);
                        break;
                }
            }
        }
    }

    void ProcessPutRing(Stack<int> baseStack)
    {
        if (ReturnTopRingSize(baseStack) == 0 || ReturnTopRingSize(baseStack) > selectedRingSize)
        {
            baseStack.Push(selectedRingSize);
            selectedRing.transform.position = this.transform.Find("base" + selected_BaseNumber).Find("RingPos" + baseStack.Count).position;
            SetCurrentBase(selectedRing, selected_BaseNumber);
            canSelectRing = true;
            canMoveRing = false;

            // 이 부분은 어떻게 처리할지 생각해볼것, 제자리에 돌려놓아도 카운트할지 말지.
            // 제자리에 돌려놓으면 카운트 안함.
            // ring이 원래있던 baseNumber와 ring이 이동할 basenumber가 같다면
            if(pre_selected_BaseNumber != selected_BaseNumber)
                tryCount++;

            // 낙장 불입 카운트. 한번집으면 무조건 카운트
            //tryCount++;
        }
    }

    void SetCurrentBase(GameObject sRing, int sBaseNum)
    {
        switch (sRing.name)
        {
            case "LargeRing":
                largeRingCurrentBase = sBaseNum;
                break;
            case "MiddleRing":
                middleRingCurrentBase = sBaseNum;
                break;
            case "SmallRing":
                smallRingCurrentBase = sBaseNum;
                break;
        }
    }

    // 새로고침 (모두 초기화)
    void Refresh()
    {
        // 모든 스택 초기화
        for(int i = 0; i < base1Stack.Count; i++)
        {
            base1Stack.Pop();
        }
        for (int i = 0; i < base2Stack.Count; i++)
        {
            base2Stack.Pop();
        }
        for (int i = 0; i < base3Stack.Count; i++)
        {
            base3Stack.Pop();
        }

        base1Stack.Push(largeRingSize); // 3
        base1Stack.Push(middleRingSize); // 2
        base1Stack.Push(smallRingSize); // 1

        // ring 위치 초기화
        largeRing.transform.position = this.transform.Find("base1").Find("RingPos1").position;
        middleRing.transform.position = this.transform.Find("base1").Find("RingPos2").position;
        smallRing.transform.position = this.transform.Find("base1").Find("RingPos3").position;

        // 하노이 탑 관련 모든 변수 초기화
        canSelectRing = true; // 선택후 움직일때 false, 링을 놓았을때 true
        canMoveRing = false; // 선택 후 true, 링을 놓았을때 false
        canMove = false;

        pre_selected_BaseNumber = 0;
        selected_BaseNumber = 0;
        max_BaseNumber = 3;
        passPutRingFuncCount = 0;
        selectedRingSize = 0;

        // Large Ring
        largeRingSize = 3;
        canSelectLargeRing = false;
        largeRingCurrentBase = 1;

        // Middle Ring
        middleRingSize = 2;
        canSelectMiddleRing = false;
        middleRingCurrentBase = 1;

        // Small Ring
        smallRingSize = 1;
        canSelectSmallRing = false;
        smallRingCurrentBase = 1;

        tryCount = 0;
    }

    public bool CheckAnswer()
    {
        if(base3Stack.Count == 3 && tryCount <= 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
