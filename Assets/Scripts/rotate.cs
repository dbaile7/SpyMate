using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.SceneManagement;

public class rotate : MonoBehaviour {

    // Use this for initialization

    SocketIOComponent socket;

    void Start()
    {
        socket = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
    }

	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}

}
