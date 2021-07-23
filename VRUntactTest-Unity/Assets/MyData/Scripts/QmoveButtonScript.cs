using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QmoveButtonScript : MonoBehaviour
{
    public SceneManagement sceneManagement;
    public Image btn1;
    public Image btn2;
    public Image btn3;
    public Image btn4;
    public Image btn5;
    public Image btn6;
    public Image btn7;
    public Image btn8;
    public Image btn9;
    public Image btn10;

    public PlayerCameraRaycast playerRaycast;
    public GameObject joyStickPointer;

    int selected_QuestionNumber;
    public Image selecting_Cursor;
    bool canMove;
    bool canUseJoyStickPointer = true;
    

    // Start is called before the first frame update
    void Start()
    {
        selected_QuestionNumber = 1;
        selecting_Cursor.gameObject.transform.localPosition = this.transform.Find("btn_Canvas").Find("btn" + selected_QuestionNumber).localPosition;
        selecting_Cursor.gameObject.transform.localPosition
                = new Vector3(selecting_Cursor.gameObject.transform.localPosition.x + 10, selecting_Cursor.gameObject.transform.localPosition.y - 10, selecting_Cursor.gameObject.transform.localPosition.z);
        canMove = false;
    }

    void Update()
    {
        //현재 문제는 노란색으로 표시, 나머지는 흰색
        for (int i = 1; i <= sceneManagement.max_QuestionNumber; i++)
        {
            if(i == sceneManagement.current_QustionNumber)
            {
                this.transform.Find("btn_Canvas").Find("btn" + i).GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                this.transform.Find("btn_Canvas").Find("btn" + i).GetComponent<Image>().color = Color.white;
            }
        }

        if (playerRaycast.ray_hitted_GameObject != null && playerRaycast.ray_hitted_GameObject.name == "QmoveButton")
        {
            if(joyStickPointer.activeSelf == true && canUseJoyStickPointer)
            {
                joyStickPointer.SetActive(false);
                canUseJoyStickPointer = !canUseJoyStickPointer;
            }
            else if (canUseJoyStickPointer)
            {
                canUseJoyStickPointer = !canUseJoyStickPointer;
            }
            MovePoint();
            SelectQuestion();
        }
        else
        {
            if(joyStickPointer.activeSelf == false && canUseJoyStickPointer ==false)
            {
                joyStickPointer.SetActive(true);
                canUseJoyStickPointer = !canUseJoyStickPointer;
            }
        }
    }

    public bool ReturnCanUseJoyStickPointer()
    {
        return canUseJoyStickPointer;
    }

    void MovePoint()
    {
        float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        Debug.Log("h현재값: "+ h);
        if (h > 0 && canMove)
        {
            //딱한번만 오른쪽으로 이동
            //다음 번호의 문제의 위치로 커서를 이동 (포지션 변경)
            if (selected_QuestionNumber < sceneManagement.max_QuestionNumber)
            {
                selected_QuestionNumber += 1;
            }
            selecting_Cursor.gameObject.transform.localPosition = this.transform.Find("btn_Canvas").Find("btn" + selected_QuestionNumber).localPosition;
            selecting_Cursor.gameObject.transform.localPosition
                = new Vector3(selecting_Cursor.gameObject.transform.localPosition.x + 10, selecting_Cursor.gameObject.transform.localPosition.y - 10, selecting_Cursor.gameObject.transform.localPosition.z);
            canMove = false;
            //Debug.Log("키보드:우측키, 상태: "+ canMove);
        }
        else if (h < 0 && canMove)
        {
            //딱한번만 왼쪽으로 이동
            if (selected_QuestionNumber > 1)
            {
                selected_QuestionNumber -= 1;
            }
            selecting_Cursor.gameObject.transform.localPosition = this.transform.Find("btn_Canvas").Find("btn" + selected_QuestionNumber).localPosition;
            selecting_Cursor.gameObject.transform.localPosition
                = new Vector3(selecting_Cursor.gameObject.transform.localPosition.x + 10, selecting_Cursor.gameObject.transform.localPosition.y - 10, selecting_Cursor.gameObject.transform.localPosition.z);
            canMove = false;
            //Debug.Log("키보드:좌측키, 상태: " + canMove);
        }
        else if (h == 0)
        {
            canMove = true;
        }


    }

    void SelectQuestion()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            sceneManagement.current_QustionNumber = selected_QuestionNumber;
        }
    }
}
