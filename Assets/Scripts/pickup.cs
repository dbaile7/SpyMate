using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (((GameObject.FindGameObjectWithTag ("Socket")).GetComponent<socket> ()).hasHunted ()) {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, Vector3.up, 2.0f);
	}

}
