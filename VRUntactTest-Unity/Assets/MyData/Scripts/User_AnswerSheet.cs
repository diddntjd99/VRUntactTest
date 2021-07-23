using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User_AnswerSheet : MonoBehaviour
{
    //public Q1_Toggle_Script q1_Toggle_Script;
    public EnglishQuestionScript englishQuestionScript;

    public Q2_Text_Script q2_Text_Script;
    public Q3_Toggle_Script q3_Toggle_Script;
    public Q4_Toggle_Script q4_Toggle_Script;

    public HanoiTowerScript hanoiTowerScript;

    public RiverCrossingScript riverCrossingScript;

    public Q7_Script q7_Script;

    public Q9_Text_Script q9_Text_Script;

    public IcosahedronScript icosahedronScript;

    public Eulerian_Path_Manager eulerian_Path_Manager;

    public bool q1_User_Answer;
    public string q2_User_Answer;
    public string q3_User_Answer;
    public string q4_User_Answer;
    public bool q5_User_Answer;
    public bool q6_User_Answer;
    public bool q7_User_Answer;

    public string q9_User_Answer;
    public bool q8_User_Answer;
    public bool q10_User_Answer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (englishQuestionScript.CheckAnswer())
        {
            q1_User_Answer = true;
        }
        else
        {
            q1_User_Answer = false;
        }

        q2_User_Answer = q2_Text_Script.q2_Answer;
        q3_User_Answer = q3_Toggle_Script.q3_Answer;
        q4_User_Answer = q4_Toggle_Script.q4_Answer;

        if (hanoiTowerScript.CheckAnswer())
        {
            q5_User_Answer = true;
        }
        else
        {
            q5_User_Answer = false;
        }

        if (riverCrossingScript.CheckAnswer())
        {
            q6_User_Answer = true;
        }
        else
        {
            q6_User_Answer = false;
        }
        if (q7_Script.CheckAnswer())
        {
            q7_User_Answer = true;
        }
        else
        {
            q7_User_Answer = false;
        }
        if (icosahedronScript.CheckAnswer())
        {
            q8_User_Answer = true;
        }
        else
        {
            q8_User_Answer = false;
        }
        if (eulerian_Path_Manager.CheckAnswer())
        {
            q10_User_Answer = true;
        }
        else
        {
            q10_User_Answer = false;
        }

        q9_User_Answer = q9_Text_Script.q9_Answer;

        //Debug.Log("q1 답:" + q1_User_Answer);
        //Debug.Log("q2 답:" + q2_User_Answer);
        //Debug.Log("q3 답:" + q3_User_Answer);
        //Debug.Log("q6 답: " + q6_User_Answer);
        //Debug.Log("q7 답: " + q7_User_Answer);
        //Debug.Log("q8 답: " + q8_User_Answer);
        //Debug.Log("q9_답: " + q9_User_Answer);
    }
}
