using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Threading;

using SocketIO;

public class dial_controller : MonoBehaviour {

	//COMBO: 30 80 180 <-- to beat the game

	//gameobjects used in script
	public GameObject dial;
	public GameObject safeDoor;
	public GameObject hinge;
	public GameObject prize;

	//audio sources
	public AudioSource tick1;
	public AudioSource tick2;

	//text boxes
	public Text winnerTextBox;
	public Text instructions;

	//lock combinations - (in degrees)
	public int combo1 = 253; // <-- 30 on the lock
	public int combo2 = 70;  // <-- 80 on the lock
	public int combo3 = 180; // <-- 50 on the lock

	//bools to check if the correct digits have been met
	bool firstNumHit = false;
	bool secondNumHit = false;
	bool thirdNumHit = false;

	//other bools used
	bool clockwise = false;
	bool safeOpen = false;
	bool gameStarted = false;

	//variable used when rotating door open
	int rotateDoor = 0;

	//socket used for communication between server
	private SocketIOComponent socketIO;

	//initialize text boxes
	void Start(){
		winnerTextBox.text = "Rotate dial clockwise to first digit, rotate counter-clockwise to second digt, and then again clockwise to last digit.";
		instructions.text = "Press space bar to attempt combo\nPress 'r' to restart";

        socketIO = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
    }

	//reset the lock by reseting all necessary variables
	void ResetLock(){
		firstNumHit = false;
		secondNumHit = false;
		thirdNumHit = false;
		gameStarted = false;
		winnerTextBox.text = "";
	}

	//this function is called repeatedly from within FixedUpdate() when the game is complete
	void OpenSafe(float angle){
		safeDoor.transform.RotateAround(hinge.transform.position, Vector3.up, angle);
	}
		
	void SendMessageToServer (string message){
		socketIO.Emit (message);
	}

	void FixedUpdate(){

		//the angle variable stores the angle of the dial
		float angle = transform.rotation.eulerAngles.z;
		angle = Mathf.Floor (angle);

		//at the start of the game clear the text box
		if (!safeOpen) {
			if (gameStarted) {
				winnerTextBox.text = "";
			}

			//move dial clockwise
			if (Input.GetKey (KeyCode.RightArrow)) {
				dial.transform.Rotate (Vector3.back);
				//create and play audio source
				AudioSource tick = GetComponent<AudioSource> ();
				if (!tick.isPlaying) {
					//tick.Play ();
					SendMessageToServer ("SafeTick"); //send small tick to server
				}
				clockwise = true;
				gameStarted = true;
			}

			//rotate dial counter-clockwise
			if (Input.GetKey (KeyCode.LeftArrow)) {
				dial.transform.Rotate (Vector3.forward);
				AudioSource tick = GetComponent<AudioSource> ();
				if (!tick.isPlaying) {
					//tick.Play ();
					SendMessageToServer ("SafeTick"); //send small tick to server
				}
				clockwise = false;
			}

			//pressing space checks to see if your combo is correct
			if (Input.GetKey (KeyCode.Space)) {
				if (firstNumHit && secondNumHit && thirdNumHit) {
					//CONGRATS - you win
					instructions.text = "";
					safeOpen = true;
					socketIO.Emit ("SafeVictory");
				} else {
					ResetLock ();
					winnerTextBox.text = "Incorrect Combo! - Lock reset";
					//rotate dial back to its original position
					transform.eulerAngles = new Vector3 (0, 0, 0);
				}
			}

			//pressing 'r' resets lock
			if (Input.GetKey (KeyCode.R)) {
				ResetLock ();
				//and rotates dial back to its original position
				transform.eulerAngles = new Vector3 (0, 0, 0);
			}
		}

		//makes sure the sound only ticks the first time it is reached
		if (angle == combo1 && !firstNumHit) {
			//tick2.Play ();
			SendMessageToServer("SafeTock");
		}
		if (angle == combo2 && !secondNumHit) {
			//tick2.Play ();
			SendMessageToServer("SafeTock");
		}
		if (angle == combo3 && !thirdNumHit) {
			//tick2.Play ();
			SendMessageToServer("SafeTock");
		}

		//for combo1: the dial must be rotating clockwise
		if (angle == combo1 && clockwise) {
			print ("First number hit!");
			firstNumHit = true;
		}

		//for combo2: combo1 must already be reached and the dial must be rotating counter-clockwise
		if (angle == combo2 && firstNumHit && !clockwise) {
			print ("Second number hit!");
			secondNumHit = true;
		}

		//for combo3: combo1 and combo2 must already be reached and the dial must be rotating clockwise
		if (angle == combo3 && firstNumHit && secondNumHit && clockwise) {
			print ("third number hit!");
			thirdNumHit = true;
		}

		//the next 3 if statements check to see if the dial has spun too far
		if(firstNumHit && !secondNumHit && clockwise){
			//if the user spins past combo1 reset the lock
			if (angle < (combo1 - 5)) { //the dial can spin past 5 degrees until failure
				print ("fail");
				ResetLock ();
			}
		}

		if (firstNumHit && secondNumHit && !thirdNumHit && !clockwise) {
			//if the user spins past combo2 reset the lock
			if (angle > (combo2 + 5)) { //the dial can spin past 5 degrees until failure
				print ("fail");
				ResetLock ();
			}
		}

		if (firstNumHit && secondNumHit && thirdNumHit && clockwise) {
			//if the user spins past combo3 reset the lock
			if (angle < (combo3 - 5)) { //the dial can spin past 5 degrees until failure
				print ("fail");
				ResetLock ();
			}
		}

		//used to animate safe door being opened
		if (safeOpen) {
			rotateDoor += 1;
			//stops calling OpenSafe() when the door has been rotated 140 degrees
			if (rotateDoor <= 140) {
				OpenSafe (1.0f);
			}
		}

		//rotate the prize
		prize.transform.RotateAround(prize.transform.position, Vector3.up, -2.0f);
        
    }
}
