using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishQuestionScript : MonoBehaviour
{
    public GameObject picture;
    public GameObject picture_CenterWall;
    public GameObject picture_RightWall;
    public GameObject picture_LeftWall;
    public GameObject book;
    public GameObject book_Bed;
    public GameObject book_Desk;
    public GameObject book_Table;
    public GameObject clock;
    public GameObject clock_Bed;
    public GameObject clock_Drawer;
    public GameObject clock_Table;
    public GameObject mug;
    public GameObject mug_Floor;
    public GameObject mug_Desk;
    public GameObject mug_Bed;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    public GameObject joyStickPointer;
    public QmoveButtonScript qmoveButtonScript;

    // picture 을 놓을 수 있는 위치
    List<GameObject> pictureList = new List<GameObject>();
    List<GameObject> bookList = new List<GameObject>();
    List<GameObject> clockList = new List<GameObject>();
    List<GameObject> mugList = new List<GameObject>();
    int pictureCurrentIndex = 1; // 현재 중앙에 위치
    int bookCurrentIndex = 1; // 현재 중앙에 위치
    int clockCurrentIndex = 1; // 현재 중앙에 위치
    int mugCurrentIndex = 1; // 현재 중앙에 위치

    bool canMoveObject = false;
    int clickCount = 0;
    bool canMove = true;
    bool isOutlined = false;

    int canMoveObjectIndex = -1; // 움직일 수 있는 오브젝트의 번호 (액자 0번, 책 1번, 시계 2번, 컵 3번)

    Transform[] pictureChildren;
    Transform[] bookChildren;
    Transform[] clockChildren;
    Transform[] mugChildren;

    // Start is called before the first frame update
    void Start()
    {

        pictureChildren = picture.GetComponentsInChildren<Transform>();
        bookChildren = book.GetComponentsInChildren<Transform>();
        clockChildren = clock.GetComponentsInChildren<Transform>();
        mugChildren = mug.GetComponentsInChildren<Transform>();
        isOutlined = false;
        DisableOutline(pictureChildren, 4);
        DisableOutline(bookChildren, 4);
        DisableOutline(clockChildren, 8);
        DisableOutline(mugChildren, 5);


        pictureList.Add(picture_LeftWall);
        pictureList.Add(picture_CenterWall);
        pictureList.Add(picture_RightWall);

        bookList.Add(book_Desk);
        bookList.Add(book_Bed);
        bookList.Add(book_Table);

        clockList.Add(clock_Drawer);
        clockList.Add(clock_Bed);
        clockList.Add(clock_Table);

        mugList.Add(mug_Desk);
        mugList.Add(mug_Floor);
        mugList.Add(mug_Bed);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!qmoveButtonScript.ReturnCanUseJoyStickPointer())
        //{
        //    return;
        //}
        if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.layer == LayerMask.NameToLayer("English_Q"))
        {
            switch (joyStick_Pointer_Script.ray_hitted_GameObject.name)
            {
                case "book":
                    DisableOutline(pictureChildren, 4);
                    EnableOutline(bookChildren, 4);
                    DisableOutline(clockChildren, 8);
                    DisableOutline(mugChildren, 5);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        clickCount += 1;
                        joyStickPointer.SetActive(false);
                        canMoveObject = true;
                        canMoveObjectIndex = 1;
                    }
                    break;
                case "Clock":
                    DisableOutline(pictureChildren, 4);
                    DisableOutline(bookChildren, 4);
                    EnableOutline(clockChildren, 8);
                    DisableOutline(mugChildren, 5);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        clickCount += 1;
                        joyStickPointer.SetActive(false);
                        canMoveObject = true;
                        canMoveObjectIndex = 2;
                    }
                    break;
                case "mug":
                    DisableOutline(pictureChildren, 4);
                    DisableOutline(bookChildren, 4);
                    DisableOutline(clockChildren, 8);
                    EnableOutline(mugChildren, 5);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        clickCount += 1;
                        joyStickPointer.SetActive(false);
                        canMoveObject = true;
                        canMoveObjectIndex = 3;
                    }
                    break;
                case "Picture":
                    EnableOutline(pictureChildren, 4);
                    DisableOutline(bookChildren, 4);
                    DisableOutline(clockChildren, 8);
                    DisableOutline(mugChildren, 5);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        clickCount += 1;
                        joyStickPointer.SetActive(false);
                        canMoveObject = true;
                        canMoveObjectIndex = 0;
                    }
                    break;
            }
        }
        else
        {
            DisableOutline(pictureChildren, 4);
            DisableOutline(bookChildren, 4);
            DisableOutline(clockChildren, 8);
            DisableOutline(mugChildren, 5);
        }

        if (canMoveObject)
        {
            switch (canMoveObjectIndex)
            {
                case 0:
                    // 액자 움직이기
                    pictureCurrentIndex = MoveObject(picture, pictureList, pictureCurrentIndex);
                    break;
                case 1:
                    // 책 움직이기
                    bookCurrentIndex = MoveObject(book, bookList, bookCurrentIndex);
                    break;
                case 2:
                    // 시계 움직이기
                    clockCurrentIndex = MoveObject(clock, clockList, clockCurrentIndex);
                    break;
                case 3:
                    // 컵 움직이기
                    mugCurrentIndex = MoveObject(mug, mugList, mugCurrentIndex);
                    break;
            }
        }
    }

    int MoveObject(GameObject gameObj, List<GameObject> gameObjList, int objCurrentIndex)
    {
        float h = Input.GetAxisRaw("Horizontal");
        // 오른쪽, 물건 클릭해놓고 시선을 문제 선택패널로 옮겨서 문제 이동시킨 다음에 다시 1번문제로 돌아오면
        // 손가락 포인터가 생기면서 물체랑 같이 이동하는 버그 현상 생겼었으나, 조건문에 함수 추가함 으로써 해결.
        if (h > 0 && canMove && qmoveButtonScript.ReturnCanUseJoyStickPointer())
        {
            objCurrentIndex += 1;
            if (objCurrentIndex > 2)
            {
                objCurrentIndex = 0;
            }
            gameObj.transform.localPosition = gameObjList[objCurrentIndex].transform.localPosition;
            gameObj.transform.localRotation = gameObjList[objCurrentIndex].transform.localRotation;
            canMove = false;
        }
        //왼쪽
        else if (h < 0 && canMove && qmoveButtonScript.ReturnCanUseJoyStickPointer())
        {
            objCurrentIndex -= 1;
            if (objCurrentIndex < 0)
            {
                objCurrentIndex = 2;
            }
            gameObj.transform.localPosition = gameObjList[objCurrentIndex].transform.localPosition;
            gameObj.transform.localRotation = gameObjList[objCurrentIndex].transform.localRotation;
            canMove = false;
        }
        // 가운데
        else if (h == 0 && qmoveButtonScript.ReturnCanUseJoyStickPointer())
        {
            canMove = true;
        }

        // 한번 더 클릭해서 물체 놓기
        if (Input.GetButtonDown("Fire1") && clickCount >= 2)
        {
            clickCount = 0;
            canMoveObject = false;
            // 만약 물건 클릭한 상태로 문제 선택패널의 다른문제 클릭시, 눈이 문제 선택패널을 향하고 있어도
            // 손가락 포인터가 사라지지 않는 버그현상 발생. 아래의 코드로 문제해결.
            if (!qmoveButtonScript.ReturnCanUseJoyStickPointer())
            {
                joyStickPointer.SetActive(false);
            }
            else
            {
                joyStickPointer.SetActive(true);
            }
        }

        return objCurrentIndex;
    }

    void EnableOutline(Transform[] children, int index)
    {
        for (int i = 3; i < index; i++)
        {
            children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = true;
        }
        isOutlined = true;
    }
    void DisableOutline(Transform[] children, int index)
    {
        for (int i = 3; i < index; i++)
        {
            children[i].gameObject.GetComponent<cakeslice.Outline>().enabled = false;
        }
        isOutlined = false;
    }

    public bool CheckAnswer()
    {
        if (pictureCurrentIndex == 2 && bookCurrentIndex == 0 && clockCurrentIndex == 2 && mugCurrentIndex == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
