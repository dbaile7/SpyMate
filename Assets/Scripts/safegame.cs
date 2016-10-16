using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.SceneManagement;

public class safegame : MonoBehaviour {

    //socket used for communication between server
    private SocketIOComponent socket;

    public GameObject safe;

    // Use this for initialization
    void Start () {
        socket = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
    }
	
	// Update is called once per frame
	void Update () {
        //Start game
        if (Input.GetKey(KeyCode.E) )
        {
            if (Vector3.Distance(gameObject.transform.position, safe.transform.position) < 5)
            {
                SceneManager.LoadScene("SafeScene");
                socket.Emit("StartSafeGame");
            }
        }
    }
}
