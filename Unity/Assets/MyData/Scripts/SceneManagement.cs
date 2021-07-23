using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    public Texture2D cursorArrow;

    int previous_QuestionNumber = 0; // 맨처음 디폴트값
    public int current_QustionNumber = 1; // 1번 문제
    public int max_QuestionNumber = 10;

    public GameObject Q1_Text;
    public GameObject EnglishQuestion;
    //public GameObject Q1_Answer_Toggle;
    public GameObject Q2_Text;
    public GameObject Q2_Answer_Text;
    public GameObject Q2_Answer_Text2;
    public GameObject Q3_Image;
    public GameObject Q3_Answer_Toggle;

    public GameObject DrawPaper;
    public GameObject DrawLineManager;
    public GameObject VR_Keyboard;

    public GameObject Arrow_Right;
    public GameObject Arrow_Left;

    public GameObject Q5_Text;
    public GameObject Q5_Text2;
    public GameObject Q5_Text3;
    public GameObject Q5_Text_MoveCount;
    public GameObject TowerOfHanoi;

    public GameObject Q4_Text;
    public GameObject Q4_Answer_Toggle;
    public GameObject Shape1;
    public GameObject RotateManager;

    public GameObject MyDrawing;

    public GameObject Q6_Text;
    public GameObject Q6_Text2;
    public GameObject Q6_Text_ResultText;
    public GameObject RiverCrossing;

    public GameObject Q7_Text;
    public GameObject Q7_Text2;
    public GameObject Q7_Text_A;
    public GameObject Q7_Text_B;
    public GameObject Q7_Text_C;
    public GameObject Q7_Text_D;
    public GameObject Q7_Color_Toggle;
    public GameObject Element;

    public GameObject Q9_CutCone;
    public GameObject Q9_Text;
    public GameObject Q9_Text2;
    public GameObject Q9_Answer_Text;
    public GameObject Q9_Answer_Text2;
    public GameObject Q9_Keyboard_Control;

    public GameObject Q8_Text;
    public GameObject Q8_Text2;
    public GameObject Q8_Color_Toggle;
    public GameObject Icosahedron;

    public GameObject Q9_Question_Icon;
    public GameObject Q9_Keyboard_Icon;

    public GameObject Q10_Shape;
    public GameObject Q10_Text;
    public GameObject Q10_SolCheck;
    public GameObject Q10_RefreshButton;
    public GameObject Q10_StartMark;

    public GameObject trash;
    public GameObject eraser;
    public GameObject pencil;

    public GameObject ListeningManager;
    public GameObject example1;
    public GameObject example2;
    public GameObject example3;

    List<GameObject> gameObject_List = new List<GameObject>();

    // enum은 지정한 문자를 숫자(인덱스)로 사용할수 있게 해줌. 사용법: (int)GOs.Q1_Text ( = 0과 동일 )
    // 주의: 순서가 꼬이면 안된다 (추가는 뒤에다만 할 것)
    enum GOs { Q1_Text, EnglishQuestion, Q2_Text, Q2_Answer_Text, Q2_Answer_Text2, 
        Q3_Image, Q3_Answer_Toggle, DrawPaper, DrawLineManager, VR_Keyboard, 
        Arrow_Right, Arrow_Left, Q5_Text, Q5_Text2, 
        Q5_Text3, Q5_Text_MoveCount, TowerOfHanoi, Q4_Text, Q4_Answer_Toggle,
        Shape1, RotateManager, MyDrawing, Q6_Text, Q6_Text2,
        Q6_Text_ResultText, RiverCrossing, Q7_Text, Q7_Text2, Q7_Text_A,
        Q7_Text_B, Q7_Text_C, Q7_Text_D, Q7_Color_Toggle, Element,
        Q9_CutCone, Q9_Text, Q9_Text2, Q9_Answer_Text, Q9_Answer_Text2,
        Q9_Keyboard_Control, Q8_Text, Q8_Text2, Q8_Color_Toggle, Icosahedron,
        Q9_Question_Icon, Q9_Keyboard_Icon, Q10_Shape, Q10_Text,
        Q10_SolCheck, Q10_RefreshButton, Q10_StartMark, trash, eraser,
        pencil, ListeningManager, example1, example2, example3
    }

    void Start()
    {
        // Cursor.visible = false;//마우스 커서 보이기
        Cursor.SetCursor(cursorArrow, Vector3.zero, CursorMode.ForceSoftware);

        gameObject_List.Add(Q1_Text);
        gameObject_List.Add(EnglishQuestion);
        gameObject_List.Add(Q2_Text);
        gameObject_List.Add(Q2_Answer_Text);
        gameObject_List.Add(Q2_Answer_Text2);
        gameObject_List.Add(Q3_Image);
        gameObject_List.Add(Q3_Answer_Toggle);

        gameObject_List.Add(DrawPaper);
        gameObject_List.Add(DrawLineManager);
        gameObject_List.Add(VR_Keyboard);

        gameObject_List.Add(Arrow_Right);
        gameObject_List.Add(Arrow_Left);

        gameObject_List.Add(Q5_Text);
        gameObject_List.Add(Q5_Text2);
        gameObject_List.Add(Q5_Text3);
        gameObject_List.Add(Q5_Text_MoveCount);
        gameObject_List.Add(TowerOfHanoi);

        gameObject_List.Add(Q4_Text); 
        gameObject_List.Add(Q4_Answer_Toggle);
        gameObject_List.Add(Shape1);
        gameObject_List.Add(RotateManager);

        gameObject_List.Add(MyDrawing);

        gameObject_List.Add(Q6_Text);
        gameObject_List.Add(Q6_Text2);
        gameObject_List.Add(Q6_Text_ResultText);
        gameObject_List.Add(RiverCrossing);

        gameObject_List.Add(Q7_Text);
        gameObject_List.Add(Q7_Text2);
        gameObject_List.Add(Q7_Text_A);
        gameObject_List.Add(Q7_Text_B);
        gameObject_List.Add(Q7_Text_C);
        gameObject_List.Add(Q7_Text_D);
        gameObject_List.Add(Q7_Color_Toggle);
        gameObject_List.Add(Element);

        gameObject_List.Add(Q9_CutCone);
        gameObject_List.Add(Q9_Text);
        gameObject_List.Add(Q9_Text2);
        gameObject_List.Add(Q9_Answer_Text);
        gameObject_List.Add(Q9_Answer_Text2);
        gameObject_List.Add(Q9_Keyboard_Control);

        gameObject_List.Add(Q8_Text);
        gameObject_List.Add(Q8_Text2);
        gameObject_List.Add(Q8_Color_Toggle);
        gameObject_List.Add(Icosahedron);

        gameObject_List.Add(Q9_Question_Icon);
        gameObject_List.Add(Q9_Keyboard_Icon);

        gameObject_List.Add(Q10_Shape);
        gameObject_List.Add(Q10_Text);
        gameObject_List.Add(Q10_SolCheck);
        gameObject_List.Add(Q10_RefreshButton);
        gameObject_List.Add(Q10_StartMark);

        gameObject_List.Add(trash);
        gameObject_List.Add(eraser);
        gameObject_List.Add(pencil);

        gameObject_List.Add(ListeningManager);
        gameObject_List.Add(example1);
        gameObject_List.Add(example2);
        gameObject_List.Add(example3);

    }

    // Update is called once per frame
    void Update()
    {
        if(previous_QuestionNumber != current_QustionNumber)
        {
            ShowQuestion(current_QustionNumber);
        }
        previous_QuestionNumber = current_QustionNumber;
    }

    void ShowQuestion(int current_QuestionNum)
    {
        DeactivateAll();

        switch (current_QuestionNum)
        {
            case 1:
                Activate_GameObject(new int[] { (int)GOs.Q1_Text, (int)GOs.EnglishQuestion, (int)GOs.ListeningManager, (int)GOs.example1, (int)GOs.example2, (int)GOs.example3, (int)GOs.Arrow_Right });
                break;
            case 2:
                Activate_GameObject(new int[] { (int)GOs.Q2_Text, (int)GOs.Q2_Answer_Text, (int)GOs.Q2_Answer_Text2, (int)GOs.VR_Keyboard, (int)GOs.Arrow_Right, (int)GOs.Arrow_Left });
                break;
            case 3:
                Activate_GameObject(new int[] { (int)GOs.trash, (int)GOs.eraser, (int)GOs.pencil, (int)GOs.Q3_Image, (int)GOs.Q3_Answer_Toggle, (int)GOs.DrawPaper, (int)GOs.DrawLineManager, (int)GOs.Arrow_Right, (int)GOs.Arrow_Left, (int)GOs.MyDrawing });
                break;
            case 4:
                Activate_GameObject(new int[] { (int)GOs.Arrow_Left, (int)GOs.Arrow_Right, (int)GOs.Shape1, 
                    (int)GOs.Q4_Text, (int)GOs.Q4_Answer_Toggle, (int)GOs.RotateManager });
                break;
            case 5:
                Activate_GameObject(new int[] { (int)GOs.Q5_Text, (int)GOs.Q5_Text2, (int)GOs.Q5_Text3, (int)GOs.Q5_Text_MoveCount, (int)GOs.TowerOfHanoi, (int)GOs.Arrow_Right, (int)GOs.Arrow_Left });
                break;
            case 6:
                Activate_GameObject(new int[] { (int)GOs.Q6_Text, (int)GOs.Q6_Text2, (int)GOs.Q6_Text_ResultText, (int)GOs.RiverCrossing, (int)GOs.Arrow_Right, (int)GOs.Arrow_Left });
                break;
            case 7:
                Activate_GameObject(new int[] { (int)GOs.Q7_Text, (int)GOs.Q7_Text2, (int)GOs.Q7_Text_A, (int)GOs.Q7_Text_B, (int)GOs.Q7_Text_C, (int)GOs.Q7_Text_D, (int)GOs.Q7_Color_Toggle, (int)GOs.Element, (int)GOs.Arrow_Right, (int)GOs.Arrow_Left });
                break;
            case 8:
                Activate_GameObject(new int[] { (int)GOs.Q8_Text, (int)GOs.Q8_Text2, (int)GOs.Q8_Color_Toggle, (int)GOs.Icosahedron, (int)GOs.Arrow_Right, (int)GOs.Arrow_Left });
                break;
            case 9:
                Activate_GameObject(new int[] { (int)GOs.Q9_CutCone, (int)GOs.Q9_Text, (int)GOs.Q9_Text2, (int)GOs.Q9_Answer_Text, (int)GOs.Q9_Answer_Text2, (int)GOs.Q9_Keyboard_Control, (int)GOs.Q9_Question_Icon, (int)GOs.Q9_Keyboard_Icon, (int)GOs.Arrow_Right, (int)GOs.Arrow_Left });
                break;
            case 10:
                Activate_GameObject(new int[] { (int)GOs.Q10_Shape, (int)GOs.Q10_Text, (int)GOs.Q10_SolCheck, (int)GOs.Q10_RefreshButton, (int)GOs.Q10_StartMark, (int)GOs.Arrow_Left });
                break;
        }
    }

    void Activate_GameObject(int[] intArr)
    {
        for (int i = 0; i < intArr.Length; i++)
        {
            for(int j = 0; j < gameObject_List.Count; j++)
            {
                if(intArr[i] == j)
                {
                    gameObject_List[j].SetActive(true);
                }
            }
        }
    }
    
    void DeactivateAll()
    {
        for(int i = 0; i < gameObject_List.Count; i++)
        {
            gameObject_List[i].SetActive(false);
        }
        
    }

}
