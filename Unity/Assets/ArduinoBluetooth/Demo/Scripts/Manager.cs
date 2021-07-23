using System;
using System.Collections;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	// Use this for initialization
	BluetoothHelper bluetoothHelper;
	string deviceName;
	[SerializeField]
	Toggle Toggle_isDevicePaired;
	[SerializeField]
	Toggle Toggle_isConnected;
	[SerializeField]
	GameObject DebugHolder;
	[SerializeField]
	Button Btn_Connect;
	[SerializeField]
	Button Btn_Disconnect;

	public Text Txt_Door;

	public JoyStick_Pointer_Script joyStick_Pointer_Script;
	public GameObject SceneManager;
	public GameObject AnswerSend_Manager;
	public GameObject SEND_Text;
	public GameObject QmoveButton;
	public GameObject Cube;
	public GameObject TimeText;
	public GameObject TimeText2;
	public GameObject Arduino_Connect_Text;
	public GameObject Test_Start_Button;
	public GameObject Test_Start_Text;
	public GameObject Connect_Button;
	public GameObject Connect_Button_Text;
	string received_message;


	// 외부에서 접근하여 사용할 수 있습니다.
	// 외부 다른 클래스에서 Manager 함수로 접근하여 Start 펑션에서 다음과 같이 사용하시면 됩니다.
	//===============================================
	// Manager.onDoorOpen.AddListener(OnDoorOpen);
	// Manager.onDoorClose.AddListener(OnDoorClose);
	//===============================================
	public UnityEvent onDoorOpen, onDoorClose;

	int count = 0;
	public bool dq = false;

	void Start () {

		//if (bluetoothHelper.isDevicePaired())
		//{
		//	Debug.Log("try to connect");
		//	bluetoothHelper.Connect(); // tries to connect
		//}
		//else
		//{
		//	Debug.Log("not DevicePaired");
		//}

		//Btn_Connect.onClick.AddListener (() => {
		//	if (bluetoothHelper.isDevicePaired ()) {
		//		Debug.Log ("try to connect");
		//		bluetoothHelper.Connect (); // tries to connect
		//	} else {
		//		Debug.Log ("not DevicePaired");
		//	}
		//});
		//Btn_Disconnect.onClick.AddListener (() => {
		//	bluetoothHelper.Disconnect ();
		//	Debug.Log ("try to Disconnect");
		//});

		//=============================================================================================
		//=============================================================================================

		deviceName = "HC-06"; //bluetooth should be turned ON; // 페어링되는 아두이노 블루투스 이름과 같아야 합니다.
		
		//=============================================================================================
		//=============================================================================================
		try {
			bluetoothHelper = BluetoothHelper.GetInstance (deviceName);
			bluetoothHelper.OnConnected += OnConnected;
			bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
			bluetoothHelper.OnDataReceived += OnMessageReceived; //read the data

			bluetoothHelper.setTerminatorBasedStream ("\n");

			if (bluetoothHelper.isDevicePaired ())
				Toggle_isDevicePaired.isOn = true;
			else
				Toggle_isDevicePaired.isOn = false;
		} catch (Exception ex) {
			Toggle_isDevicePaired.isOn = false;
			//Debug.Log (ex.Message);
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Alpha0)) {
			if (!isDebugOn) {
				isDebugOn = true;
				DebugHolder.SetActive (true);
			} else {
				isDebugOn = false;
				DebugHolder.SetActive (false);
				myLog = "reset";
			}
		}

		if (joyStick_Pointer_Script.ray_hitted_GameObject != null && joyStick_Pointer_Script.ray_hitted_GameObject.name == "Connect_Button")
		{
			if (Input.GetButtonDown("Fire1"))
			{
				if (bluetoothHelper.isDevicePaired())
				{
					//Debug.Log("try to connect");
					bluetoothHelper.Connect(); // tries to connect
				}
				else
				{
					//Debug.Log("not DevicePaired");
				}
			}
		}


		// 테스트 start 버튼 누르면 테스트 시작
		if (joyStick_Pointer_Script.ray_hitted_GameObject!= null && joyStick_Pointer_Script.ray_hitted_GameObject.name == "Test_Start_Button")
		{
			if (Input.GetButtonDown("Fire1"))
			{
				AnswerSend_Manager.SetActive(true);
				SEND_Text.SetActive(true);
				SceneManager.SetActive(true);
				QmoveButton.SetActive(true);
				Cube.SetActive(true);
				TimeText.SetActive(true);
				TimeText2.SetActive(true);
				Test_Start_Button.SetActive(false);
				Test_Start_Text.SetActive(false);
				Arduino_Connect_Text.SetActive(false);
			}
		}
	}

	//Asynchronous method to receive messages
	void OnMessageReceived () {
		received_message = bluetoothHelper.Read ();
		//Debug.Log(received_message);

		if (received_message.Contains ("on")) {
			Txt_Door.text = "Door is close";
			onDoorClose.Invoke();
		}

		if (received_message.Contains ("off")) {
			Txt_Door.text = "Door is open";
			onDoorOpen.Invoke();
		}

		// 아두이노 코드에서 거리 측정 후 '실격' 메세지 보내면 처리 dq: disqualification(실격)
		if (received_message.Contains("dq"))
		{
			if(count == 0)
			{
				// 실격 처리
				dq = true;
			}

			count += 1;
		}
	}

	void OnConnected () {
		Toggle_isConnected.isOn = true;
		try {
			bluetoothHelper.StartListening ();
			//Debug.Log ("Connected");

			// 연결 되었으면 테스트 시작 버튼 보임.
			Arduino_Connect_Text.GetComponent<Text>().text = "아두이노 연결 상태:  연결 됨";

			Test_Start_Button.SetActive(true);
			Test_Start_Text.SetActive(true);
			Connect_Button.SetActive(false);
			Connect_Button_Text.SetActive(false);

		} catch (Exception ex) {
			//Debug.Log (ex.Message);
		}
	}

	void OnConnectionFailed () {
		Toggle_isConnected.isOn = false;
		Debug.Log ("Connection Failed");
	}

	//Call this function to emulate message receiving from bluetooth while debugging on your PC.
	void OnGUI () {
		if (isDebugOn) {
			if (bluetoothHelper != null)
				bluetoothHelper.DrawGUI ();
			else
				return;

			if (!bluetoothHelper.isConnected ()) {
				Btn_Connect.interactable = true;
				Btn_Disconnect.interactable = false;
				Toggle_isConnected.isOn = false;
			}

			if (bluetoothHelper.isConnected ()) {
				Btn_Connect.interactable = false;
				Btn_Disconnect.interactable = true;
				Toggle_isConnected.isOn = true;
			}

			// Screen Debug
            /*
			GUIStyle myStyle = new GUIStyle ();
			myStyle.fontSize = 16;
			myStyle.normal.textColor = Color.blue;
			GUI.Label (new Rect (10, 100, 1080, 1920), myLog, myStyle);
            */
		}
	}

	void OnDestroy () {
		if (bluetoothHelper != null)
			bluetoothHelper.Disconnect ();
	}

	void OnApplicationQuit () {
		if (bluetoothHelper != null)
			bluetoothHelper.Disconnect ();
	}

	// Screen Debug
	bool isDebugOn = true;
	string myLog;
	Queue myLogQueue = new Queue ();
	void OnEnable () {
		Application.logMessageReceived += HandleLog;
	}
	void OnDisable () {
		Application.logMessageReceived -= HandleLog;
	}
	void HandleLog (string logString, string stackTrace, LogType type) {
		myLog = logString;
		string newString = "[" + type + "] : " + myLog + "\n";
		myLogQueue.Enqueue (newString);
		if (type == LogType.Exception) {
			newString = "\n" + stackTrace;
			myLogQueue.Enqueue (newString);
		}
		myLog = string.Empty;
		foreach (string mylog in myLogQueue) {
			myLog += mylog;
		}
	}
}