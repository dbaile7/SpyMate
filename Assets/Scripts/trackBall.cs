using UnityEngine;
using System.Collections;

public class trackBall : MonoBehaviour {

	public GameObject ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = new Vector3(ball.transform.position.x, 
			24, ball.transform.position.z); 

		transform.position = newPos;
	}
}
