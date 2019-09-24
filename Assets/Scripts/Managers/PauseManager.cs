using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
	public GameObject restartButton;
	public GameObject exitToMenuButton;

	public GameObject exitYesButton;
	public GameObject exitNoButton;

	public GameObject restartYesButton;
	public GameObject restartNoButton;	

	public GameObject resumeButton;

	public GameObject pauseButton;

	public bool paused;
	public bool gameOver;

	public Text centerText;
	public GameObject CenterText;

	public GameObject CanvasObject;

	private PlayerController playerController;
	public PlayerControllerAndroid playerControllerAndroid;

	public float transitionDuration;
	public Camera mainCamera;

	private Scene currentScene;
	private bool currentlyGameOver;

	public bool developerMode;

	private bool amAndroid;

	void Update() {
		if (!amAndroid) {
			if (Input.GetKeyDown (KeyCode.Escape) && !paused) {
				pauseButtonFunction ();
			} else if (paused && Input.GetKeyDown (KeyCode.Escape)) {
				Start ();
				Time.timeScale = 1;
				centerText.text = "";
			}
		}

		if(gameOver&& !currentlyGameOver){
			End ();
			currentlyGameOver = true;
		}

		if(developerMode) {
			if (Input.GetKeyDown (KeyCode.BackQuote))
				CanvasObject.SetActive (CanvasObject.activeSelf ? false : true);
		}

	}
		
	void Start () {
		if(SceneManager.GetActiveScene ().name == "MainAndroid" ||SceneManager.GetActiveScene ().name == "Race_Game_Android") {
			amAndroid = true;
			playerControllerAndroid = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerAndroid> ();
		} else { 
			amAndroid = false;
			playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}

		CanvasObject.SetActive (true);
		if (!amAndroid) 
			playerController.enabled = true;
		else 
			playerControllerAndroid.enabled = true;

		resumeButton.SetActive (false);
		restartButton.SetActive (false);
		exitToMenuButton.SetActive (false);
		exitNoButton.SetActive (false);
		exitYesButton.SetActive (false);
		restartYesButton.SetActive (false);
		restartNoButton.SetActive (false);
		pauseButton.SetActive (true);
		paused = false;
		mainCamera.fieldOfView = 60f;
		currentScene = SceneManager.GetActiveScene ();
		currentlyGameOver = false;
	}

	//These are all button functions and their related functions. This can probably be cleaner, but why though.

	public void resumeButtonFunction () {
		Start ();
		Time.timeScale = 1;
		centerText.text = "";
	}

	public void pauseButtonFunction () {
		Time.timeScale = 0;
		paused = true;
		if (!amAndroid)
			centerText.text = "GAME PAUSED \n \"ESC\" to resume";
		else
			centerText.text = "GAME PAUSED \n Tap \"Resume\" to unpause";
		exitToMenuButton.SetActive (true);
		restartButton.SetActive (true);
		pauseButton.SetActive (false);

		if(!gameOver)
			resumeButton.SetActive (true);
	}

	public void restartButtonFunction() {
		exitToMenuButton.SetActive (false);
		restartButton.SetActive (false);
		resumeButton.SetActive (false);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void restartYesButtonFunction () {
		Time.timeScale = 1;
		StartCoroutine (transition (currentScene.buildIndex));
	}

	public void restartNoButtonFunction () {
		if (!gameOver) {
			if (!amAndroid)
				centerText.text = "GAME PAUSED \n \"ESC\" to resume";
			else
				centerText.text = "GAME PAUSED \n Tap \"Resume\" to unpause";
		} else
			centerText.text = "GAME OVER";
		exitToMenuButton.SetActive (true);
		restartButton.SetActive (true);
		restartNoButton.SetActive (false);
		restartYesButton.SetActive (false);
		if(!gameOver)
			resumeButton.SetActive (true);
	}

	public void exitButtonFunction() {
		centerText.text = "Are you sure\n you want to Exit?";
		exitToMenuButton.SetActive (false);
		restartButton.SetActive (false); 
		exitYesButton.SetActive (true);
		exitNoButton.SetActive (true);
		resumeButton.SetActive (false);
	}

	public void exitYesButtonFunction (){
		exitYesButton.SetActive (false);
		exitNoButton.SetActive (false);
		CenterText.SetActive (false);
		Time.timeScale = 1;
        //ADS.ShowAd();
		StartCoroutine (transition (0));
	}

	public void exitNoButtonFunction() {
		if (!gameOver) {
			if (!amAndroid)
				centerText.text = "GAME PAUSED \n \"ESC\" to resume";
			else
				centerText.text = "GAME PAUSED \n Tap \"Resume\" to unpause";
		} else
			centerText.text = "GAME OVER";
		exitNoButton.SetActive (false);
		exitYesButton.SetActive (false);
		exitToMenuButton.SetActive (true);
		restartButton.SetActive (true);
		if(!gameOver)
			resumeButton.SetActive (true);
	}

	public void End() {
		restartButton.SetActive (true);
		exitToMenuButton.SetActive (true);
		pauseButton.SetActive (false);
		if (amAndroid)
			playerControllerAndroid.enabled = false;
		else
			playerController.enabled = false;
		gameOver = true;
	}

	IEnumerator transition(int Scene) {
		CanvasObject.SetActive (false);
		if (amAndroid)
			playerControllerAndroid.enabled = false;
		else
			playerController.enabled = false;
		float t = 0.0f;
		while (t < 1.0) { 
			mainCamera.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 150f, Time.deltaTime * 5);
			t += Time.deltaTime * (Time.timeScale / transitionDuration);
			yield return 0;
		}
		Time.timeScale = 0;
		SceneManager.LoadScene (Scene);
	}
}
