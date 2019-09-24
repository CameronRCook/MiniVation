using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public Image boostAmountBar;
	public GameObject boostFlames;
	public GameObject player;

	public GameObject captureProgress;
	public Image captureProgressBar;

	public Text DayCounter;

	public float years;
	public float timeAlive;

	public Text centerText;
	public GameObject CenterText;
	public PauseManager pauseManager;
	private bool currentGameOverSelection;
	public int score;
	public GameObject subManager;
	private bool amAndroid;
	public bool start;
	//Need to fill this out and add a disable/enable if statement. Same with explosion vibration.
	public GameObject music;


	// Use this for initialization
	void Start () {
		captureProgress.SetActive (false);

		GameObject.FindGameObjectWithTag ("Music").GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat("musicVolume");

		timeAlive = 0;
		Time.timeScale = 1;
		currentGameOverSelection = false;
		if(SceneManager.GetActiveScene ().name == "MainAndroid"){
			amAndroid = true;
		}
		StartCoroutine (welcome ());

		if(PlayerPrefs.GetInt ("MusicStatus")==0){
			music.SetActive (true);
		}
		else{
			music.SetActive (false);
		}
	
	}

	IEnumerator welcome() {
		captureProgress.SetActive (false);
		centerText.text = "Defend The Planet\n Destroy The Enemy Planes!";
		yield return new WaitForSeconds (5);
		SpawnZone ();
		start = true;
		centerText.text = "Enemies Inbound!";
		yield return new WaitForSeconds (3);
		centerText.text = "";
	}

	// Update is called once per frame
	void Update () {
		//Health bar code.
		timeAlive += (1 * Time.deltaTime);
		DayCounter.text = "Score: " + score;
		if (!amAndroid) {
			boostAmountBar.fillAmount = player.GetComponent<PlayerController> ().boostFuel / 100.0f;
		}
		else if(amAndroid){
			boostAmountBar.fillAmount = player.GetComponent<PlayerControllerAndroid> ().boostFuel / 100.0f;
		}
	}

	public void GameOver(){
		RemoveEnemies ();
		CenterText.SetActive (true);

		if (score > PlayerPrefs.GetInt ("combatHighScore"))
			PlayerPrefs.SetInt ("combatHighScore", score);
		Debug.Log (PlayerPrefs.GetInt ("combatHighScore"));

		Time.timeScale = 0;
		//ADS.ShowAd ();
		currentGameOverSelection = true;
		pauseManager.gameOver = true;
		centerText.text = "Game Over\nScore " + score + "\nHigh Score " + PlayerPrefs.GetInt("combatHighScore") + "\nRestart";
	}

	public void IncreaseScore(int amount){
		score = score + amount;
	}

	public void SpawnZone(){
		subManager.GetComponent<SpawnTarget> ().spawnTarget ();
	}

	public void ZoneCompleted(){
		SpawnZone ();
		Debug.Log ("spawning");
		//Will add more stuff here such as adding score, timeing, and effects.

	}

	public void RemoveEnemies(){
		foreach (GameObject a in GameObject.FindGameObjectsWithTag ("Enemy"))
			Destroy (a);
	}
}
