using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneObjective : MonoBehaviour {

	private SpawnTarget sMan;
	private GameManager gMan;

	private bool inZone;
	public float captureTime;
	private float currentTimeing;
	private float checkpoint;

	private Image zoneHealthBar;

	// Use this for initialization
	void Start () {
		inZone = false;
		gMan = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		sMan = GameObject.FindGameObjectWithTag ("SubManager").GetComponent<SpawnTarget> (); 
		currentTimeing = captureTime;
		checkpoint = captureTime / 2;
		//zoneHealthBar = GameObject.FindGameObjectWithTag ("ZoneCompletionBar").GetComponent<Image> ();
		//zoneHealthBar.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (inZone) {
			gMan.captureProgress.SetActive (true);
			currentTimeing -= Time.deltaTime;
			if (currentTimeing <= 0) {
				Finished ();
			}
			gMan.captureProgressBar.fillAmount = (1 - (currentTimeing / captureTime));
		}
		//Resets timer if the player leaves.
		else if (currentTimeing != checkpoint || currentTimeing != captureTime) {
			if (currentTimeing <= checkpoint)
				currentTimeing = checkpoint;
			else
				currentTimeing = captureTime;
		}		
			if (!inZone)
				gMan.captureProgress.SetActive (false);
	}
	
		void OnTriggerEnter(Collider other){
		if(other.tag ==  "Player"){
			inZone = true;
		}
	}
	//This causes the reset if the player leaves the area. 
	void OnTriggerExit(Collider other){
		if(other.tag=="Player"){
			inZone = false;
		}
	}

	void Finished(){
		gMan.ZoneCompleted ();
		sMan.inProgress = false;
		gMan.IncreaseScore (10);
		Destroy (this.gameObject);
	} 

}
