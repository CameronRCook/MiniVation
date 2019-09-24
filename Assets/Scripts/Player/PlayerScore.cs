using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {
	public RaceGameManager raceGameManager;
	public GameObject fireworkEffect;
	public GameObject fireworkSound;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("target")) {
			Instantiate (fireworkSound,other.transform.position,other.transform.rotation);
			Instantiate (fireworkEffect, other.transform.position,other.transform.rotation);
			raceGameManager.scored ();
			Debug.Log("Score");
			Destroy (other.gameObject);

		}
	}
}
