using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

	public float totalTime;
	private float currentTime;

	// Use this for initialization
	void Start () {
		currentTime = totalTime;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime -= Time.deltaTime;
		if(currentTime<=0){
			Destroy (this.gameObject);
		}
	}
}
