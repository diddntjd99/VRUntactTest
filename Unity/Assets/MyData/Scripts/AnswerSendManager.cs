using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnswerSendManager : MonoBehaviour
{
    public Project.Networking.NetworkClient netWorkClient;
    public JoyStick_Pointer_Script joyStick_Pointer_Script;
    public GameObject sendObject;
    public LoginTextScript loginTextScript;
    float examTimeLimit = 1800f;
    int minute;
    int second;
    bool testEnd = false;

    public User_AnswerSheet userAnswerSheet;
    public Text timeText;

    public Manager manager;

    [Serializable]
    public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        List<TKey> keys;
        [SerializeField]
        List<TValue> values;

        Dictionary<TKey, TValue> target;
        public Dictionary<TKey, TValue> ToDictionary() { return target; }

        public Serialization(Dictionary<TKey, TValue> target)
        {
            this.target = target;
        }

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(target.Keys);
            values = new List<TValue>(target.Values);
        }

        public void OnAfterDeserialize()
        {
            var count = Math.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (var i = 0; i < count; ++i)
            {
                target.Add(keys[i], values[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(examTimeLimit);

        // 실격이면
        if(manager.dq == true)
        {
            testEnd = true;
            var userAnswer = new Dictionary<string, string>();
            userAnswer.Add("학번", loginTextScript.studentID.ToString());
            userAnswer.Add("문제1", "실격 처리");
            userAnswer.Add("문제2", "실격 처리");
            userAnswer.Add("문제3", "실격 처리");
            userAnswer.Add("문제4", "실격 처리");
            userAnswer.Add("문제5", "실격 처리");
            userAnswer.Add("문제6", "실격 처리");
            userAnswer.Add("문제7", "실격 처리");
            userAnswer.Add("문제8", "실격 처리");
            userAnswer.Add("문제9", "실격 처리");
            userAnswer.Add("문제10", "실격 처리");

            String str = JsonUtility.ToJson(new Serialization<string, string>(userAnswer));
            netWorkClient.Emit("msg", new JSONObject(JsonUtility.ToJson(new Serialization<string, string>(userAnswer))));
            Debug.Log(str);
            // 실격 씬 내보냄
            SceneManager.LoadScene("Test_DQ");
        }

        if(examTimeLimit > 0f)
        {
            examTimeLimit -= Time.deltaTime;
            minute = (int)examTimeLimit / 60;
            second = (int)examTimeLimit % 60;
            timeText.text = string.Format("{0:D2}:{1:D2}", minute, second);
        }
        // 테스트 종료 및 답안 데이터 전송
        else if(examTimeLimit <= 0f && testEnd == false)
        {
            SendDataAndEndTest();
            minute = (int)examTimeLimit / 60;
            second = (int)examTimeLimit % 60;
            timeText.text = string.Format("{0:D2}:{1:D2}", minute, second);
            // 종료 씬 내보냄
            SceneManager.LoadScene("TestEnd");

        }

        if(joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.name == sendObject.name)
        {
            //Debug.Log("123");
            sendObject.GetComponent<cakeslice.Outline>().enabled = true;
            if (Input.GetButtonDown("Fire1"))
            {
                if(testEnd == false)
                {
                    SendDataAndEndTest();
                    // 종료 씬 내보냄
                    SceneManager.LoadScene("TestEnd");
                }
            }
        }
        else
        {
            if(sendObject.GetComponent<cakeslice.Outline>().enabled == true)
            {
                sendObject.GetComponent<cakeslice.Outline>().enabled = false;
            }
        }
        
    }

    void SendDataAndEndTest()
    {
        testEnd = true;
        var userAnswer = new Dictionary<string, string>();
        userAnswer.Add("학번", loginTextScript.studentID.ToString());
        userAnswer.Add("문제1", userAnswerSheet.q1_User_Answer.ToString());
        userAnswer.Add("문제2", userAnswerSheet.q2_User_Answer.ToString());
        userAnswer.Add("문제3", userAnswerSheet.q3_User_Answer.ToString());
        userAnswer.Add("문제4", userAnswerSheet.q4_User_Answer.ToString());
        userAnswer.Add("문제5", userAnswerSheet.q5_User_Answer.ToString());
        userAnswer.Add("문제6", userAnswerSheet.q6_User_Answer.ToString());
        userAnswer.Add("문제7", userAnswerSheet.q7_User_Answer.ToString());
        userAnswer.Add("문제8", userAnswerSheet.q8_User_Answer.ToString());
        userAnswer.Add("문제9", userAnswerSheet.q9_User_Answer.ToString());
        userAnswer.Add("문제10", userAnswerSheet.q10_User_Answer.ToString());

        String str = JsonUtility.ToJson(new Serialization<string, string>(userAnswer));
        netWorkClient.Emit("msg", new JSONObject(JsonUtility.ToJson(new Serialization<string, string>(userAnswer))));
        Debug.Log(str);
    }
}
