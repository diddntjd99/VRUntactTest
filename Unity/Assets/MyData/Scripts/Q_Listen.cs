using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Q_Listen : MonoBehaviour
{
    public JoyStick_Pointer_Script joyStick_Pointer_Script;

    public GameObject EX1;
    public GameObject EX2;
    public GameObject EX3;
    AudioSource audioSource1;
    AudioSource audioSource2;
    AudioSource audioSource3;

    public void Start()
    {
        if (joyStick_Pointer_Script.ray_hitted_GameObject != null)
        {
            ////음... 음...엄............

            //audioSource = GetComponent<AudioSource>();

            //audioSource.clip = English_Sentence1; //오디오에 bgm이라는 파일 연결

            //audioSource.volume = 1.0f; //0.0f ~ 1.0f사이의 숫자로 볼륨을 조절
            //audioSource.loop = false; //반복 여부
            //audioSource.mute = false; //오디오 음소거

            //audioSource.Play(); //오디오 재생
            //audioSource.Stop(); //오디오 멈추기

            //audioSource.playOnAwake = false;
            ////활성화시 해당씬 실행시 바로 사운드 재생이 시작
            ////비활성화시 Play()명령을 통해서만 재생

            //audioSource.priority = 0;
            ////씬안에 모든 오디오소스중 현재 오디오 소스의 우선순위를 정한다.
            //// 0 : 최우선, 256 : 최하, 128 : 기본값
        }
        audioSource1 = EX1.GetComponent<AudioSource>();
        audioSource2 = EX2.GetComponent<AudioSource>();
        audioSource3 = EX3.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(joyStick_Pointer_Script.ray_hitted_GameObject != null)
        {
            switch (joyStick_Pointer_Script.ray_hitted_GameObject.name)
            {
                case "EX1":
                    EX1.GetComponent<MeshRenderer>().material.color = Color.green;
                    EX2.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    EX3.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // 오디오1 재생
                        audioSource1.Play();
                        //Debug.Log("Play1");
                    }
                    break;
                case "EX2":
                    EX1.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    EX2.GetComponent<MeshRenderer>().material.color = Color.green;
                    EX3.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // 오디오2 재생
                        audioSource2.Play();
                    }
                    break;
                case "EX3":
                    EX1.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    EX2.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    EX3.GetComponent<MeshRenderer>().material.color = Color.green;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // 오디오3 재생
                        audioSource3.Play();
                    }
                    break;
                default:
                    EX1.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    EX2.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    EX3.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;
            }
        }
        else
        {
            EX1.GetComponent<MeshRenderer>().material.color = Color.yellow;
            EX2.GetComponent<MeshRenderer>().material.color = Color.yellow;
            EX3.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }

}