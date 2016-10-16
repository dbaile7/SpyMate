using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using SocketIO;

public class doorgame : MonoBehaviour {

    //socket used for communication between server
    private SocketIOComponent socket;

	public Text instructions;

    public GameObject door;

    // Use this for initialization
    void Start () {
        socket = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
    }
	
	// Update is called once per frame
	void Update () {
        //Start game
		if (Vector3.Distance (gameObject.transform.position, door.transform.position) < 5) {
			instructions.text = "Press E";
			if (Input.GetKey (KeyCode.E)) {
				if (((GameObject.FindGameObjectWithTag ("Socket")).GetComponent<socket> ()).hasHunted ()) {
					SceneManager.LoadScene ("pinball");
					instructions.text = "";

					socket.Emit ("StartTiltGame");
				} else {
					instructions.text = "You need a key";
				}
			}
		} else {
			instructions.text = "";
		}
    }
}
