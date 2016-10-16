using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using SocketIO;

public class player_controller : MonoBehaviour {

	public float speed = 15.0f;
	public GameObject target;
	public GameObject targetPrefab;
	public Rigidbody rocket;
	public Transform cam;
	public GameObject hinge;
	public float sensitivity = 1.0f;
	public Text startText;
	public SocketIOComponent socketIO;

	private Vector3 lastMousePosition = new Vector3(0,0,0);
//	private Vector3 initialCamPos = new Vector3(0,0,0);
//	private Vector3 initialTargetPos = new Vector3(0,0,0);
//	private Quaternion initialTargetRot;

	private int count = 0;
//	private bool gameStart = false;
//	private int checkStart = 0;

	private static float x;
	private static float y;

	bool shoot = false;

	void Start() {
		socketIO = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
		startText.text = "Press 's' to start.";
//		initialCamPos = cam.transform.position;	
//		initialTargetPos = target.transform.position;
//		initialTargetRot = target.transform.rotation;
		socketIO.On ("TouchXY", MoveTurret);
	}

	void FireRocket () {
		if (count % 5 == 0) {
			Rigidbody rocketClone = (Rigidbody)Instantiate (rocket, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			rocketClone.velocity = transform.forward * speed;
		}
	} 
		
	void MoveTurret(SocketIOEvent e){
		x = float.Parse (e.data.GetField ("X").str);
		y = float.Parse (e.data.GetField ("Y").str);
		print (x +","+ y);
		if (x <= lastMousePosition.x) {
			transform.RotateAround (hinge.transform.position, Vector3.up * (x - lastMousePosition.x), sensitivity);
		} else {
			transform.RotateAround (hinge.transform.position, Vector3.up * (x + lastMousePosition.x), sensitivity);
		}
		lastMousePosition = new Vector3(x, y, 0.0f);
		shoot = true;
//		FireRocket ();
	}

	void StartGame (){
		socketIO.Emit ("StartCannonGame");
		startText.text = "";
	}

	void FixedUpdate() {

		if (Input.GetKeyDown(KeyCode.S)) {
			StartGame();

		}
		if(shoot){
			FireRocket ();
			count++;
			shoot = false;
		}

		if(Input.GetMouseButton(0)){
			
			if(Input.GetMouseButtonDown(0)){
				//reset
				lastMousePosition = Input.mousePosition;  
			}
			else if(Input.mousePosition.y <= lastMousePosition.y){
				transform.RotateAround(hinge.transform.position, Vector3.up*(Input.mousePosition.x - lastMousePosition.x), sensitivity);
			}
			lastMousePosition = Input.mousePosition;
//			count++;
			FireRocket ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			print ("Restart");
			((GameObject.FindGameObjectWithTag ("score")).GetComponent<scoreController> ()).resetScore ();
			Destroy (target);

			target = Instantiate (targetPrefab);
			target.name = target.name.Replace ("(Clone)", "");
			Vector3 newPos = new Vector3(0, 0.5f, 2.5f);
			target.transform.position = newPos;
		}

	}
		
}
