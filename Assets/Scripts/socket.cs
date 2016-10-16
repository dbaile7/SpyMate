
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

using SocketIO;
using UnityEngine.SceneManagement;

public class socket : MonoBehaviour {

	public SocketIOComponent socketIO;


	public InputField connectionCodeField;
	public Text connectionText;
	public Text welcomeText;
	public Button playButton;

	private bool hunted = false;

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
    }

    private void ConnectionChecker()
    {
        while (socketIO.socket.IsAlive) ;
        Debug.Log("Closed");
        Application.Quit();
    }

    void ConnectionSuccess(SocketIOEvent ev){
		Debug.Log (ev);

        //Load the next level
        SceneManager.LoadScene("MainScene");

        Object.DontDestroyOnLoad(gameObject);
        Object.DontDestroyOnLoad(socketIO.gameObject);
    }

	void ConnectionLost(SocketIOEvent ev){
		Debug.Log (ev);

        
    }

	public void hunt()
	{
		hunted = true;
	}

	public bool hasHunted()
	{
		return hunted;
	}

    public SocketIOComponent getSocket()
    {
        return socketIO;
    }
}
