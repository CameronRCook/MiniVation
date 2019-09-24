using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDetection : MonoBehaviour {
	public Vector3 directionUp;
	// Use this for initialization
	private bool grown;

	void Start () {
		grown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!grown) {
			this.transform.localScale += new Vector3 (0f, .5f, .0f) * Time.deltaTime;
		}
		if(this.transform.localScale.y>=3.5){
			grown = true;
		}
	}
	void OnCollissionStay(Collision c){
		Debug.Log ("Inside first");
		if (c.gameObject.CompareTag ("InnerCollider")) {
			Debug.Log ("Triggering");
			directionUp = c.contacts [0].point - transform.position;
			directionUp = -directionUp;
		}
	}
	void OnCollissionEnter(Collision other){
		Debug.Log ("inside the enter");
	}
}
