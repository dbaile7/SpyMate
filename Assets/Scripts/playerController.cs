using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SocketIO;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

	public float speed;

    SocketIOComponent socketio;

    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		socketio = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();

    }

    void Update()
    {
        //float moveH = Input.GetAxis("Horizontal") * -1;
        //float moveV = Input.GetAxis("Vertical") * -1;
        //Vector3 movement = new Vector3(moveH, 0.0f, moveV);
        //rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("goal"))
        {
            other.gameObject.SetActive(false);
			print ("victory");
            SceneManager.LoadScene("Maze");
			print ("flex");
			socketio.Emit("TiltGameVictory");
        }
    }

}
