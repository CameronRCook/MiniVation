using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceGameManager : MonoBehaviour {
	public Image boostAmountBar;
	public GameObject boostFlames;

	public PlayerController playerController;
	
	//These are in seconds
	public float timeLimit, timeLeft;
	public Text Timer;

	public PauseManager pauseManager;

	public Text centerText;
	public GameObject CenterText;

	public SpawnTarget spawnTargets;
	public int Score;
	public Text scoreText;

	public bool refueling;
	public bool boostFull;
	public bool ticking;
	public float boostCountDown;
	public Text boostText;

	public bool ended;


	private bool amAndroid;
	//public GameObject music;
	public GameObject player;
	public GameObject body;

	// Use this for initialization
	void Start () {
        timeLeft = timeLimit + 5;
		Time.timeScale = 1;

		GameObject.FindGameObjectWithTag ("Music").GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat("musicVolume");

		Score = 0;
		scoreText.text = "Score: " + Score;
		refueling = false;
		boostCountDown = 5;
		ended = false;
		if(SceneManager.GetActiveScene ().name == "Race_Game_Android"){
			amAndroid = true;
		} else{
			amAndroid = false;
		}

		/*
		if(PlayerPrefs.GetInt ("MusicStatus")==0){
			music.SetActive (true);
		}
		else{
			music.SetActive (false);
		}
		*/
	}
		
	// Update is called once per frame
	void Update () {
		if (!amAndroid) {
			boostAmountBar.fillAmount = player.GetComponent<PlayerController> ().boostFuel / 100.0f;
		}
		else if(amAndroid){
			boostAmountBar.fillAmount = player.GetComponent<PlayerControllerAndroid> ().boostFuel / 100.0f;
		}

		if (timeLeft <= 0 && !ended) {
			endOfGame ();
		}

		if(timeLeft <= 0)
			Timer.text = "OUT OF TIME";
		else if (timeLeft < 60) {
			Timer.text = ((int)timeLeft + 1) + " Seconds";
		} else {
			Timer.text = (int)(timeLeft / 60) + " Minutes " + (int)((timeLeft % 60) + 1) + " Seconds";
		}
	
		timeLeft -= (1 * Time.deltaTime);
	}

	void LateUpdate() {
		if(!amAndroid)
		{
			boostFull = (playerController.boostFuel >= 100 ? true : false);
		}
		else if(amAndroid){
			boostFull = (player.GetComponent<PlayerControllerAndroid> ().boostFuel >= 100 ? true : false);

		}
			
		if (!ended) {
			if (boostFull) {
				refueling = false;
				ticking = false;
				if (!amAndroid)
					boostText.text = "Boost Full: Shift";
				else
					boostText.text = "Boost Full";
			} else {
				boostText.text = "Stop Boosting to Refuel!";
			}
		}

		boost ();

		if(refueling) {
			boostCountDown -= (1 * Time.deltaTime);
			if (!amAndroid) {
				if (boostCountDown <= 0 && playerController.boostFuel <= 100) {
					playerController.boostFuel += (5 * Time.deltaTime);
					ticking = false;
					boostText.text = "Boost Refueling!";
				} else {
					ticking = true;
					boostText.text = "Boost Refueling: " + (int)(boostCountDown + 1);
				}
			}
			//Edit this
			else if(amAndroid){
				if (boostCountDown <= 0 && player.GetComponent<PlayerControllerAndroid> ().boostFuel <= 100) {
					player.GetComponent<PlayerControllerAndroid> ().boostFuel += (5 * Time.deltaTime);
					ticking = false;
					boostText.text = "Boost Refueling!";
				} else {
					ticking = true;
					boostText.text = "Boost Refueling: " + (int)(boostCountDown + 1);
				}
			}
		}
	}

	public void endOfGame() {
		//ADS.ShowAd ();
		ended = true;
		Timer.text = "OUT OF TIME";
		centerText.text = "Game Over\nScore " + Score + "\nHigh Score " + PlayerPrefs.GetInt("raceHighScore") + "\nRestart or Exit?";

		if (Score > PlayerPrefs.GetInt ("raceHighScore"))
			PlayerPrefs.SetInt ("raceHighScore", Score);

		Debug.Log (PlayerPrefs.GetInt ("raceHighScore"));

		pauseManager.End ();
		Time.timeScale = 0;
		if (amAndroid)
			body.GetComponent<ShipTiltAndroid> ().enabled = false;
		else
			body.GetComponent<ShipTilt> ().enabled = true;
	}

	public void scored() {
		Score++;
		timeLeft = timeLimit;
		scoreText.text = "Score: " + Score;
		spawnTargets.inProgress = false;
	}

	public void boost() {
		if (!amAndroid) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				refueling = false;
				ticking = false;
				boostCountDown = 5;
			} else if (!boostFull && !Input.GetKey (KeyCode.LeftShift)) {
				refueling = true;
			}
		}
		else{
			if(player.GetComponent<PlayerControllerAndroid> ().boostFuel <= 0) {
				refueling = true; 
				player.GetComponent<PlayerControllerAndroid> ().boosting = false;
			}

			if (player.GetComponent<PlayerControllerAndroid> ().boosting) {
				refueling = false;
				ticking = false;
				boostCountDown = 5;
			} else if (!boostFull && !player.GetComponent<PlayerControllerAndroid> ().boosting) {
				refueling = true;
			}
		}
	}
}
