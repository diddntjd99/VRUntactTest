using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

namespace Project.Networking
{
    public class NetworkClient : SocketIOComponent
    {
        //public User_AnswerSheet userAnswerSheet;

        //[Serializable]
        //public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
        //{
        //    [SerializeField]
        //    List<TKey> keys;
        //    [SerializeField]
        //    List<TValue> values;

        //    Dictionary<TKey, TValue> target;
        //    public Dictionary<TKey, TValue> ToDictionary() { return target; }

        //    public Serialization(Dictionary<TKey, TValue> target)
        //    {
        //        this.target = target;
        //    }

        //    public void OnBeforeSerialize()
        //    {
        //        keys = new List<TKey>(target.Keys);
        //        values = new List<TValue>(target.Values);
        //    }

        //    public void OnAfterDeserialize()
        //    {
        //        var count = Math.Min(keys.Count, values.Count);
        //        target = new Dictionary<TKey, TValue>(count);
        //        for (var i = 0; i < count; ++i)
        //        {
        //            target.Add(keys[i], values[i]);
        //        }
        //    }
        //}
        

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            setupEvents();

            
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }

        private void setupEvents()
        {
            On("open", (E) =>
            {
                Debug.Log("Connection made to the server");
                
                //var userAnswer = new Dictionary<string, string>();
                //userAnswer.Add("문제1", userAnswerSheet.q1_User_Answer.ToString());
                //userAnswer.Add("문제2", userAnswerSheet.q2_User_Answer.ToString());
                //userAnswer.Add("문제3", userAnswerSheet.q3_User_Answer.ToString());
                //userAnswer.Add("문제4", userAnswerSheet.q4_User_Answer.ToString());
                //userAnswer.Add("문제5", userAnswerSheet.q5_User_Answer.ToString());
                //userAnswer.Add("문제6", userAnswerSheet.q6_User_Answer.ToString());
                //userAnswer.Add("문제7", userAnswerSheet.q7_User_Answer.ToString());
                //userAnswer.Add("문제8", userAnswerSheet.q8_User_Answer.ToString());
                //userAnswer.Add("문제9", userAnswerSheet.q9_User_Answer.ToString());
                //userAnswer.Add("문제10", userAnswerSheet.q10_User_Answer.ToString());

                //String str = JsonUtility.ToJson(new Serialization<string, string>(userAnswer));
                //Emit("msg", new JSONObject(JsonUtility.ToJson(new Serialization<string, string>(userAnswer))));
                //Debug.Log(str);
            });
        }
    }

}
