using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiverCrossingScript : MonoBehaviour
{
    List<GameObject> destination_GameObjList = new List<GameObject>();
    List<GameObject> startingPoint_GameObjList = new List<GameObject>();

    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    public QmoveButtonScript qmoveButtonScript;

    public GameObject wolf;
    public GameObject sheep;
    public GameObject grass;
    public GameObject ship;
    Vector3 shipInitPos;

    public GameObject startingPoint;
    public GameObject destination;

    GameObject previous_SelectedGameObj;
    GameObject selectedGameobj;
    bool canSelectThing = true;
    bool canMoveShip = false;
    bool isSomethingSelected = false;
    bool isShipMoving = false;
    float currentShipPos = 2; // 2는 오른쪽, 1은 왼쪽
    float shipSpeed = 60f;
    bool canProcessNextStep = true; // 문제를 계속해서 풀 수 있는 상태인가

    bool canWolfSelect = true;
    bool canSheepSelect = true;
    bool canGrassSelect = true;
    bool isFail = false;
    GameObject gameObj1;
    GameObject gameObj2;

    public Text resultText;
    string resultString = "실패";

    // Start is called before the first frame update
    void Start()
    {
        shipInitPos = ship.transform.localPosition;

        startingPoint_GameObjList.Add(wolf);
        startingPoint_GameObjList.Add(sheep);
        startingPoint_GameObjList.Add(grass);

        previous_SelectedGameObj = null;

        InitPosition(currentShipPos);
        DisableOutline(1);
        DisableOutline(2);
        DisableOutline(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!qmoveButtonScript.ReturnCanUseJoyStickPointer())
        {
            return;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Refresh();
        }
        if (!canProcessNextStep)
        {
            return;
        }
        if (canSelectThing)
        {
            SelectToMove();
        }
        if (canMoveShip)
        {
            MoveShip();
        }
        if (destination_GameObjList.Count == 3)
        {
            resultString = "성공";
        }
        else
        {
            resultString = "실패";
        }

        resultText.text = "현재 결과:  " + resultString;
    }

    public bool CheckAnswer()
    {
        if (destination_GameObjList.Count == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SelectToMove()
    {
        canMoveShip = true;
        if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("RiverCrossing"))
        {
            switch (joyStick_Pointer_Script.ray_hitted_GameObject.name)
            {
                case "Wolf":
                    DisableOutline(0);
                    DisableOutline(1);
                    EnableOutline(2);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        CheckCanSelect();
                        if (canWolfSelect)
                        {
                            // 나머지 위치 초기화
                            InitPosition(currentShipPos);
                            // 선택한 오브젝트 위치 바꾸기
                            ChangeSelectedObjectPosition(wolf, "Wolf_Destination", "Wolf_StartingPoint");
                        }
                    }
                    break;
                case "Sheep":
                    //wolf.GetComponent<MeshRenderer>().material.color = Color.white;
                    //sheep.GetComponent<MeshRenderer>().material.color = Color.red;
                    DisableOutline(0);
                    EnableOutline(1);
                    DisableOutline(2);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        CheckCanSelect();
                        if (canSheepSelect)
                        {
                            // 나머지 위치 초기화
                            InitPosition(currentShipPos);
                            // 선택한 오브젝트 위치 바꾸기
                            ChangeSelectedObjectPosition(sheep, "Sheep_Destination", "Sheep_StartingPoint");
                        }
                    }
                    break;
                case "Grass":
                    //wolf.GetComponent<MeshRenderer>().material.color = Color.white;
                    //sheep.GetComponent<MeshRenderer>().material.color = Color.white;
                    EnableOutline(0);
                    DisableOutline(1);
                    DisableOutline(2);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        CheckCanSelect();
                        if (canGrassSelect)
                        {
                            // 나머지 위치 초기화
                            InitPosition(currentShipPos);
                            // 선택한 오브젝트 위치 바꾸기
                            ChangeSelectedObjectPosition(grass, "Grass_Destination", "Grass_StartingPoint");
                        }
                    }
                    break;
            }
        }
        else
        {
            //wolf.GetComponent<MeshRenderer>().material.color = Color.white;
            //sheep.GetComponent<MeshRenderer>().material.color = Color.white;
            DisableOutline(0);
            DisableOutline(1);
            DisableOutline(2);
        }
    }

    void ChangeSelectedObjectPosition(GameObject gObj, string dest, string start)
    {
        // 배 위에 올려놓은 오브젝트와 다른걸 선택하면 다른 오브젝트를 배 위에 올림.
        if (previous_SelectedGameObj != gObj)
        {
            selectedGameobj = gObj;
            isSomethingSelected = true;
            selectedGameobj.transform.localPosition = new Vector3(ship.transform.localPosition.x, selectedGameobj.transform.localPosition.y, ship.transform.localPosition.z);
            previous_SelectedGameObj = gObj;
        }
        // 배 위에 이미 오브젝트를 올려놨는데, 다시 클릭하면 배 위에서 내림
        else
        {
            selectedGameobj = null;
            isSomethingSelected = false;
            if (currentShipPos == 1f)
            {
                previous_SelectedGameObj.transform.localPosition = this.transform.Find(dest).localPosition;
            }
            else if (currentShipPos == 2f)
            {
                previous_SelectedGameObj.transform.localPosition = this.transform.Find(start).localPosition;
            }
            previous_SelectedGameObj = null;
        }
    }

    void CheckCanSelect()
    {
        canWolfSelect = false;
        canSheepSelect = false;
        canGrassSelect = false;
        // startingPoint
        if (currentShipPos == 2f)
        {
            for (int i = 0; i < startingPoint_GameObjList.Count; i++)
            {
                if (startingPoint_GameObjList[i].name == "Wolf")
                {
                    canWolfSelect = true;
                }
                else if (startingPoint_GameObjList[i].name == "Sheep")
                {
                    canSheepSelect = true;
                }
                else if (startingPoint_GameObjList[i].name == "Grass")
                {
                    canGrassSelect = true;
                }
            }
        }
        else if (currentShipPos == 1f)
        {
            for (int i = 0; i < destination_GameObjList.Count; i++)
            {
                if (destination_GameObjList[i].name == "Wolf")
                {
                    canWolfSelect = true;
                }
                else if (destination_GameObjList[i].name == "Sheep")
                {
                    canSheepSelect = true;
                }
                else if (destination_GameObjList[i].name == "Grass")
                {
                    canGrassSelect = true;
                }
            }
        }
    }

    void InitPosition(float cShipPos)
    {
        // startingPoint
        if (cShipPos == 2f)
        {
            if (canWolfSelect)
            {
                wolf.transform.localPosition = this.transform.Find("Wolf_StartingPoint").localPosition;
            }
            if (canSheepSelect)
            {
                sheep.transform.localPosition = this.transform.Find("Sheep_StartingPoint").localPosition;
            }
            if (canGrassSelect)
            {
                grass.transform.localPosition = this.transform.Find("Grass_StartingPoint").localPosition;
            }
        }
        // destination
        if (cShipPos == 1f)
        {
            if (canWolfSelect)
            {
                wolf.transform.localPosition = this.transform.Find("Wolf_Destination").localPosition;
            }
            if (canSheepSelect)
            {
                sheep.transform.localPosition = this.transform.Find("Sheep_Destination").localPosition;
            }
            if (canGrassSelect)
            {
                grass.transform.localPosition = this.transform.Find("Grass_Destination").localPosition;
            }
        }
    }

    void MoveShip()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("Jump"))
        {
            // k 키를 누르면 배가 이동 완료할 때까지 타겟 선택 못함
            canSelectThing = false;
            isShipMoving = true;
        }

        // 배가 움직이기 시작하는 상황이라면
        if (isShipMoving)
        {
            // 배가 왼쪽에서 오른쪽으로 이동
            if (currentShipPos == 1f)
            {
                // k키 한번 누르면 좌에서 우로 배가 자동 이동.
                if (ship.transform.localPosition.x < startingPoint.transform.localPosition.x)
                {
                    ship.transform.Translate(new Vector3(1f * shipSpeed * Time.deltaTime, 0f, 0f));
                    if (isSomethingSelected)
                    {
                        selectedGameobj.transform.localPosition = new Vector3(ship.transform.localPosition.x, selectedGameobj.transform.localPosition.y, ship.transform.localPosition.z);
                    }
                }
                // 이동 완료했으면 현재 배는 오른쪽에 위치
                else
                {
                    ship.transform.localPosition = startingPoint.transform.localPosition;
                    if (isSomethingSelected)
                    {
                        // 선택한 물체 이동처리
                        switch (selectedGameobj.name)
                        {
                            case "Wolf":
                                wolf.transform.position = this.transform.Find("Wolf_StartingPoint").localPosition;
                                break;
                            case "Sheep":
                                sheep.transform.position = this.transform.Find("Sheep_StartingPoint").localPosition;
                                break;
                            case "Grass":
                                grass.transform.position = this.transform.Find("Grass_StartingPoint").localPosition;
                                break;
                        }
                    }

                    // 이동완료 후 처리
                    CompleteMoveObj(2f);
                }
            }
            // 배가 오른쪽에서 왼쪽으로 이동
            else if (currentShipPos == 2f)
            {
                // k키 한번 누르면 우에서 좌로 배가 자동 이동.
                if (ship.transform.localPosition.x > destination.transform.localPosition.x)
                {
                    ship.transform.Translate(new Vector3(-1f * shipSpeed * Time.deltaTime, 0f, 0f));
                    if (isSomethingSelected)
                    {
                        selectedGameobj.transform.localPosition = new Vector3(ship.transform.localPosition.x, selectedGameobj.transform.localPosition.y, ship.transform.localPosition.z);
                    }
                }
                // 이동 완료했으면 현재 배는 왼쪽에 위치
                else
                {
                    ship.transform.localPosition = destination.transform.localPosition;
                    if (isSomethingSelected)
                    {
                        // 선택한 물체 이동처리
                        switch (selectedGameobj.name)
                        {
                            case "Wolf":
                                wolf.transform.position = this.transform.Find("Wolf_Destination").localPosition;
                                break;
                            case "Sheep":
                                sheep.transform.position = this.transform.Find("Sheep_Destination").localPosition;
                                break;
                            case "Grass":
                                grass.transform.position = this.transform.Find("Grass_Destination").localPosition;
                                break;
                        }
                    }

                    // 이동완료 후 처리
                    CompleteMoveObj(1f);
                }
            }
        }
    }

    void CompleteMoveObj(float cShipPos)
    {
        canSelectThing = true;
        isShipMoving = false;
        currentShipPos = cShipPos;

        // 사용자가 선택한 것을 옮겨 줘야함.
        // 선택 이동처리
        ProcessSelectionMovement(currentShipPos);
        // 양이 먹히거나 양배추가 먹히는 상황인지 판별해야함.
        if (CheckFailure() == true)
        {
            // 실패
            Debug.Log("실패");
            canProcessNextStep = false;
        }
        else
        {
            Debug.Log("성공");
            // 성공
        }
    }

    void ProcessSelectionMovement(float cShipPos)
    {
        // 선택된게 있다면
        if (isSomethingSelected)
        {
            // 배가 오른쪽에서 왼쪽으로 이동한 결과 반영 (현재 배는 destination에 위치(왼쪽))
            if (cShipPos == 1f)
            {
                for (int i = 0; i < startingPoint_GameObjList.Count; i++)
                {
                    if (startingPoint_GameObjList[i].name == selectedGameobj.name)
                    {
                        // startingPoint에서 옮길 물체를 리스트에서 삭제
                        startingPoint_GameObjList.RemoveAt(i);
                        break;
                    }
                }

                // destination에 옮겨질 물체를 리스트에 추가
                destination_GameObjList.Add(selectedGameobj);
            }
            // 배가 왼쪽에서 오른쪽으로 이동한 결과 반영 (현재 배는 startingPoint에 위치(오른쪽))
            else if (cShipPos == 2f)
            {
                for (int i = 0; i < destination_GameObjList.Count; i++)
                {
                    if (destination_GameObjList[i].name == selectedGameobj.name)
                    {
                        // destination에서 옮길 물체를 리스트에서 삭제
                        destination_GameObjList.RemoveAt(i);
                        break;
                    }
                }

                // startingPoint에 옮겨질 물체를 리스트에 추가
                startingPoint_GameObjList.Add(selectedGameobj);
            }

        }
        // 선택된게 없다면 배는 그냥 움직임
        else
        {

        }

        isSomethingSelected = false;
        selectedGameobj = null;
        previous_SelectedGameObj = null;
    }

    // 리턴값이 true면 실패(다음단계 진행 불가), false면 다음 단계 진행가능
    bool CheckFailure()
    {
        // 현재 배가 왼쪽(destination)에 있을때, startingPointList의 물체를 검사
        if (currentShipPos == 1f)
        {
            // startingPoint에 (양, 양배추) 만 있거나 (늑대, 양) 만 있으면 실패
            ProcessCheckFailure(startingPoint_GameObjList);

        }
        // 현재 배가 오른쪽(startingPoint)에 있을때, destinationList의 물체를 검사
        else if (currentShipPos == 2f)
        {
            // destination에 (양, 양배추) 만 있거나 (늑대, 양) 만 있으면 실패
            ProcessCheckFailure(destination_GameObjList);
        }

        return isFail;
    }

    void ProcessCheckFailure(List<GameObject> gameObjList)
    {
        if (gameObjList.Count == 2)
        {
            gameObj1 = gameObjList[0];
            gameObj2 = gameObjList[1];
            switch (gameObj1.name)
            {
                case "Wolf":
                    if (gameObj2.name == "Sheep")
                    {
                        isFail = true;
                    }
                    break;
                case "Sheep":
                    if (gameObj2.name == "Grass" || gameObj2.name == "Wolf")
                    {
                        isFail = true;
                    }
                    break;
                case "Grass":
                    if (gameObj2.name == "Sheep")
                    {
                        isFail = true;
                    }
                    break;
            }
        }
        else
        {
            isFail = false;
        }
    }

    void Refresh()
    {
        // wolf, sheep, grass, ship 위치 초기화
        wolf.transform.localPosition = this.transform.Find("Wolf_StartingPoint").localPosition;
        sheep.transform.localPosition = this.transform.Find("Sheep_StartingPoint").localPosition;
        grass.transform.localPosition = this.transform.Find("Grass_StartingPoint").localPosition;
        ship.transform.localPosition = shipInitPos;

        // 리스트 초기화
        destination_GameObjList.Clear();
        startingPoint_GameObjList.Clear();

        startingPoint_GameObjList.Add(wolf);
        startingPoint_GameObjList.Add(sheep);
        startingPoint_GameObjList.Add(grass);

        previous_SelectedGameObj = null;
        selectedGameobj = null;
        canSelectThing = true;
        canMoveShip = false;
        isSomethingSelected = false;
        isShipMoving = false;
        currentShipPos = 2f; // 2는 오른쪽, 1은 왼쪽
        shipSpeed = 60f;
        canProcessNextStep = true; // 문제를 계속해서 풀 수 있는 상태인가

        canWolfSelect = true;
        canSheepSelect = true;
        canGrassSelect = true;
        isFail = false;
        gameObj1 = null;
        gameObj2 = null;

        resultString = "실패";
    }

    void EnableOutline(int ObjectNum)
    {
        // 양배추박스
        if (ObjectNum == 0)
        {
            grass.GetComponent<cakeslice.Outline>().enabled = true; //박스
            grass.transform.Find("Cabbage").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            grass.transform.Find("Cabbage (1)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            grass.transform.Find("Cabbage (2)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            grass.transform.Find("Cabbage (3)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            grass.transform.Find("Cabbage (4)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            grass.transform.Find("Cabbage (5)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            grass.transform.Find("Cabbage (6)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            grass.transform.Find("Cabbage (7)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
        }
        //양
        else if (ObjectNum == 1)
        {
            sheep.transform.Find("body").GetComponent<cakeslice.Outline>().enabled = true;
            sheep.transform.Find("leg").GetComponent<cakeslice.Outline>().enabled = true;
            sheep.transform.Find("Bip001").transform.Find("Bip001 Pelvis").transform.Find("Bip001 Spine")
                .transform.Find("Bip001 Tail").transform.Find("tail").GetComponent<cakeslice.Outline>().enabled = true;
        }
        else if (ObjectNum == 2)
        {
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD0").GetComponent<cakeslice.Outline>().enabled = true;
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD1").GetComponent<cakeslice.Outline>().enabled = true;
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD2").GetComponent<cakeslice.Outline>().enabled = true;
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD3").GetComponent<cakeslice.Outline>().enabled = true;
        }
    }
    void DisableOutline(int ObjectNum)
    {
        // 양배추박스
        if (ObjectNum == 0)
        {
            grass.GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage (1)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage (2)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage (3)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage (4)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage (5)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage (6)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            grass.transform.Find("Cabbage (7)").transform.Find("UCX_Food_Veg_Cabbage_01_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
        }
        //양
        else if (ObjectNum == 1)
        {
            sheep.transform.Find("body").GetComponent<cakeslice.Outline>().enabled = false;
            sheep.transform.Find("leg").GetComponent<cakeslice.Outline>().enabled = false;
            sheep.transform.Find("Bip001").transform.Find("Bip001 Pelvis").transform.Find("Bip001 Spine")
                .transform.Find("Bip001 Tail").transform.Find("tail").GetComponent<cakeslice.Outline>().enabled = false;
        }
        else if (ObjectNum == 2)
        {
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD0").GetComponent<cakeslice.Outline>().enabled = false;
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD1").GetComponent<cakeslice.Outline>().enabled = false;
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD2").GetComponent<cakeslice.Outline>().enabled = false;
            wolf.transform.Find("SK_Wolf_LodGroup").transform.Find("SK_Wolf_LOD3").GetComponent<cakeslice.Outline>().enabled = false;
        }
    }
}
