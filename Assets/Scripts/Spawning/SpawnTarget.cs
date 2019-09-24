using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTarget : MonoBehaviour {
	public int wave;
	public bool inProgress;
	public bool started;

	public Text centerText;

	public GameObject target;
	public float planetRadius;
	public Transform planet;
	// Use this for initialization
	void Start () {
		started = false;
		inProgress = false;
		StartCoroutine (startGame ());
	}
	// Update is called once per frame
	void Update () {
		if(started) {
			spawnTarget ();
		}
	}	
	public void spawnTarget() {
		if(!inProgress) { 
			inProgress = true;
			Vector3 a = Random.onUnitSphere * planetRadius;
			GameObject objective = Instantiate (target, a, Quaternion.identity, parent:planet);
			objective.transform.LookAt (planet);
			wave++;
			StartCoroutine (newWave ());
		}
	}
	IEnumerator startGame() {
		centerText.text = "Wave 1 in 5 Seconds";
		yield return new WaitForSeconds (5);
		centerText.text = "";
		started = true;
	}
	IEnumerator newWave() {
		centerText.text = "Wave " + wave;
		yield return new WaitForSeconds (3);
		centerText.text = "";
	}
}