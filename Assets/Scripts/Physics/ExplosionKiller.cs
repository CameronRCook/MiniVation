using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionKiller : MonoBehaviour {
	public float count;
	private float current;

	// Use this for initialization
	void Start () {
		current = count;
	}
	
	// Update is called once per frame
	void Update () {
		current -= Time.deltaTime;
		if(current <= 0){
			Destroy (this.gameObject);
		}

	}
}
