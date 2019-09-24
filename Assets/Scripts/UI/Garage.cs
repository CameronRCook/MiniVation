using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garage : MonoBehaviour {

	public Slider slider;
	public Dropdown mainWing;
	public Dropdown TailWing;

	public GameObject[] MainWings;
	public GameObject[] TailWings;

	// Use this for initialization
	void Start () {
		MainWings = GameObject.FindGameObjectsWithTag ("MainWing");
		for(int i = 0; i < MainWings.Length;i++) {
			MainWings [i] = GameObject.Find ("MainWing" + i);
		}
		selectMainWing ();

		TailWings = GameObject.FindGameObjectsWithTag ("TailWing");
		for(int i = 0; i < TailWings.Length;i++) {
			TailWings [i] = GameObject.Find ("TailWing" + i);
		}
		selectTailWing ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3 (0, slider.value, 0);


	}

	public void selectMainWing() {
		for(int i = 0; i < MainWings.Length;i++) {
			if(i == mainWing.value) {
				MainWings [i].SetActive (true);
			} else {
				MainWings [i].SetActive(false);
			}
		}
	}

	public void selectTailWing() {
		for(int i = 0; i < TailWings.Length;i++) {
			if(i == TailWing.value) {
				TailWings [i].SetActive (true);
			} else {
				TailWings [i].SetActive(false);
			}
		}
	}
}
