using UnityEngine;
using System.Collections;

public class bullet_controller : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= -5.0f || transform.position.z >= 5.0f) {
			Destroy(transform.gameObject);
		}
		if (transform.position.x <= -6.0f || transform.position.x >= 6.0f) {
			Destroy(transform.gameObject);
		}
	}
}
