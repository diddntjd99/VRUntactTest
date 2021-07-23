using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneManagement : MonoBehaviour
{
    public Text text;
    float time = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time < 0f)
        {
            Application.Quit();
        }
        else
        {
            time -= Time.deltaTime;
            text.text = "수고 하셨습니다.\n" + ((int)time).ToString() + "초 후 자동 종료됩니다.";
        }
    }
}
