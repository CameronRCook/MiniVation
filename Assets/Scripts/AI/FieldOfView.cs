using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
	public bool inSight;

	// Use this for initialization
	void Start () {
		inSight = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag ("Player")){
			inSight = true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.CompareTag ("Player")){
			inSight = false;
		}
	}

}
