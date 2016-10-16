using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

using SocketIO;

public class scoreController : MonoBehaviour {

	public Text scoreBoard;
	public Text winner;
	public SocketIOComponent socketIO;
	private int score;

	// Use this for initialization
	void Start () {
		socketIO = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
		score = 0;
	}
	
	public void addScore()
	{
		score++;
		print(score);
		scoreBoard.text = "Score: " + score;
		if (score == 9) {
			winner.text = "Nice Shooting!";
			SceneManager.LoadScene("MainScene");
			SceneManager.UnloadScene ("huntergame");
			Destroy(GameObject.Find("key"));
			socketIO.Emit ("CannonGameWin");

		}
		if (score == 0) {
			winner.text = "";
		}

	}
	public void resetScore(){
		score = 0;
		scoreBoard.text = "Score: " + score;
	}

	public int getScore()
	{
		return score;
	}
}
