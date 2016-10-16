using UnityEngine;
using System.Collections;

public class target_controller : MonoBehaviour {

	public float speed = 15.0f;

	public GameObject turret;


	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate(){
		transform.position += transform.up * speed * Time.deltaTime; 
	}

	// Update is called once per frame
	void Update () {
		
	}

	void SetActive(){
		print ("setting active");
		transform.gameObject.SetActive (true);
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.tag == "bullet"){
//			Destroy (transform.gameObject);
			transform.gameObject.SetActive (false);
			((GameObject.FindGameObjectWithTag("score")).GetComponent<scoreController>()).addScore();
		}
	}
}
