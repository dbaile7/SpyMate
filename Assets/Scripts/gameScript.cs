
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

using SocketIO;

public class gameScript : MonoBehaviour {

	public SocketIOComponent socketIO;


	public InputField connectionCodeField;
	public Text connectionText;
	public Text welcomeText;
	public Button playButton;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}


	//This method is called when the desktop user enters a code and presses play
	public void CreateConnectionPair (){
		if (connectionCodeField.text == "") {
			connectionText.text = "Enter valid Connection Code:";
		} else {
			connectionText.text = "Enter code on Mobile Device";
			playButton.gameObject.SetActive (false);
			connectionCodeField.enabled = false;

			Dictionary<string, string> data = new Dictionary<string, string>();
			data ["code"] = connectionCodeField.text;
			socketIO.Emit("NewDesktopConnection", new JSONObject(data));

			SocketCallbackInit ();
		}
	}

	//Set Up message functions
	void SocketCallbackInit (){
		socketIO.On ("ConnectionSuccess", ConnectionSuccess);
		socketIO.On ("ConnectionLost", ConnectionLost);

	}

	void ConnectionSuccess(SocketIOEvent ev){
		Debug.Log (ev);
	}

	void ConnectionLost(SocketIOEvent ev){
		Debug.Log (ev);
	}

}
