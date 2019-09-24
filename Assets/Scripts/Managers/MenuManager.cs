using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public GameObject playButton;
	public GameObject creditsButton;
	public GameObject settingsButton;
	public GameObject returnButton;
	public GameObject settingsText;
	public GameObject quitButton;

	public GameObject combatButton;
	public GameObject raceButton;

	public GameObject titleImage;

	public GameObject helpImg;
	public bool helpNow;
	public bool settingsNow;

	public Camera mainCamera;

	Vector3 menu = new Vector3(-18, 56,-14);
	Vector3 settings = new Vector3 (18, 56, -14);
	Vector3 help = new Vector3 (0, 56, -59);
	Vector3 game = new Vector3 (0, 56, -14);
	Vector3 levelSelect = new Vector3 (0, 56, 26);
	public static bool startGame = false;
	public float transitionDuration;

	//Settings Menu
	public GameObject explosionVibrateButton;
	public GameObject musicStatusButton;
	public GameObject sfxStatusButton;

	private bool explosionVibrateStatus;
	private bool musicStatus;

	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;

	public GameObject leaderboard;
	public Text combatHighScoreText;
	public Text raceHighScoreText;

	public GameObject music;

	public GameObject hintsButton;
	public bool hints;

	void Start() {
		PlayerPrefs.SetInt ("setup", 0);
		Debug.Log ("setup: " + PlayerPrefs.GetInt ("setup"));

		helpNow = false;
		hints = false;
		settingsNow = false;
		combatButton.SetActive (false);
		raceButton.SetActive (false);
		//Will need to change this with player prefs.
		explosionVibrateStatus = true;
		explosionVibrateButton.SetActive (false);
		musicStatusButton.SetActive (false);
		sfxStatusButton.SetActive (false);

		//Debug.Log ("setup: " + PlayerPrefs.GetInt ("setup"));
		//Debug.Log ("music Volume: " + PlayerPrefs.GetFloat ("musicVolume"));
		//Debug.Log ("sfx Volume: " + PlayerPrefs.GetFloat ("sfxVolume"));
        PlayerPrefs.SetInt("setup", 1);
		explosionVibrateButton.GetComponentInChildren<Text> ().text = (PlayerPrefs.GetInt ("ExplosionVibrate") == 0 ? "Enabled" : "Disabled");

		if (PlayerPrefs.GetInt ("setup") == 0) {
			PlayerPrefs.SetInt ("setup", 1);
			PlayerPrefs.SetFloat ("musicVolume", 1);
			PlayerPrefs.SetFloat ("sfxVolume", 1);
			musicVolumeSlider.value = PlayerPrefs.GetFloat ("musicVolume");
			sfxVolumeSlider.value = PlayerPrefs.GetFloat ("sfxVolume");
		} else {
			musicVolumeSlider.value = PlayerPrefs.GetFloat ("musicVolume");
			sfxVolumeSlider.value = PlayerPrefs.GetFloat ("sfxVolume");
		}
	}

	IEnumerator transition(Vector3 to, bool main, bool play, bool select, int scene) {
		playButton.SetActive (false);
		creditsButton.SetActive (false);
		settingsButton.SetActive (false);
		returnButton.SetActive (false);
		hintsButton.SetActive (false);
		quitButton.SetActive (false);
		titleImage.SetActive (false);

		Vector3 startingPos = mainCamera.transform.position;
		float t = 0.0f;
		while (t < 1f) {
			t += Time.deltaTime * (Time.timeScale / transitionDuration);
			mainCamera.transform.position = Vector3.Lerp (startingPos, to, t);
			if (play) {
				mainCamera.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 
					150f, Time.deltaTime * 10);
				mainCamera.transform.position = game;
			}
			yield return 0;
		}

		if (helpNow) {
			leaderboard.SetActive (true);
			combatHighScoreText.text = PlayerPrefs.GetInt ("combatHighScore").ToString();
			raceHighScoreText.text = PlayerPrefs.GetInt ("raceHighScore").ToString();
		}

		if(settingsNow) {
			if(Application.CanStreamedLevelBeLoaded("MainAndroid"))
				explosionVibrateButton.SetActive (true);
			musicStatusButton.SetActive (true);
			sfxStatusButton.SetActive (true);
		}
		else{
			explosionVibrateButton.SetActive (false);
			musicStatusButton.SetActive (false);
			sfxStatusButton.SetActive (false);
		}

		if (main) {
			playButton.SetActive (true);
			creditsButton.SetActive (true);
			settingsButton.SetActive (true);
			quitButton.SetActive (true);
			titleImage.SetActive (true);
		} else if (select) {
			combatButton.SetActive (true);
			raceButton.SetActive (true);
			returnButton.SetActive (true);
			hintsButton.SetActive (true);
		} else if (play) {
			returnButton.SetActive (false);
			combatButton.SetActive (false);
			raceButton.SetActive (false);
			hintsButton.SetActive (false);
			SceneManager.LoadScene (scene);
		} else if (!main) {
			returnButton.SetActive (true);
		} 
	}

	public void hintsButtonFunction() {
		combatButton.SetActive (false);
		raceButton.SetActive (false);
		hintsButton.SetActive (false);
		returnButton.SetActive (true);
		helpImg.SetActive (true);
		hints = true;
	}

	public void helpButtonFunction() {
		helpNow = true;
		StartCoroutine (transition (help, false, false,false, 0));
	}

	public void settingsButtonFunction(){
		StartCoroutine (transition (settings, false, false,false, 0));
		settingsNow = true;
	}

	public void combatSelect() {
		combatButton.SetActive (false);
		raceButton.SetActive (false);
		StartCoroutine (transition (game, false, true,false, 1));
	}

	public void raceSelect() {
		combatButton.SetActive (false);
		raceButton.SetActive (false);
		StartCoroutine (transition (game, false, true,false, 2));
	}

	public void playButtonFunction() {
		StartCoroutine (transition (levelSelect, false, false,true, 0));
		//hintsButton.SetActive (true);
	}

	public void returnButtonFunction() {
		if(hints) {
			combatButton.SetActive (true);
			raceButton.SetActive (true);
			returnButton.SetActive (true);
			helpImg.SetActive (false);
			hintsButton.SetActive (true);
			hints = false;

			return;
		}

		sfxStatusButton.SetActive (false);
		leaderboard.SetActive (false);
		StartCoroutine (transition (menu, true, false,false, 0));
		helpNow = false;
		helpImg.SetActive (false);
		settingsNow = false;
		settingsText.SetActive (false);
		combatButton.SetActive (false);
		raceButton.SetActive (false);
		explosionVibrateButton.SetActive (false);
		musicStatusButton.SetActive (false);
		hintsButton.SetActive (false);
	}

	public void ChangeExplosionVibrate(){
		explosionVibrateStatus = !explosionVibrateStatus;
		if(explosionVibrateStatus){
			explosionVibrateButton.GetComponentInChildren<Text> ().text = "Enabled";
			PlayerPrefs.SetInt ("ExplosionVibrate", 0);
			PlayerPrefs.Save ();

		}
		else if(!explosionVibrateStatus){
			explosionVibrateButton.GetComponentInChildren<Text> ().text = "Disabled";
			PlayerPrefs.SetInt ("ExplosionVibrate", 1);
			PlayerPrefs.Save ();
		}
	}

	public void setMusicVolume() {
		PlayerPrefs.SetFloat ("musicVolume", musicVolumeSlider.value);
		GameObject.FindGameObjectWithTag ("Music").GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat("musicVolume");
		Debug.Log ("setup: " + PlayerPrefs.GetInt ("setup"));
		Debug.Log ("music Volume: " + PlayerPrefs.GetFloat ("musicVolume"));
		Debug.Log ("sfx Volume: " + PlayerPrefs.GetFloat ("sfxVolume"));
	}

	public void setSfxVolume() {
		PlayerPrefs.SetFloat ("sfxVolume", sfxVolumeSlider.value); 
		Debug.Log ("setup: " + PlayerPrefs.GetInt ("setup"));
		Debug.Log ("music Volume: " + PlayerPrefs.GetFloat ("musicVolume"));
		Debug.Log ("sfx Volume: " + PlayerPrefs.GetFloat ("sfxVolume"));
	}

	public void ExitApplication(){
		Application.Quit ();
	}

}

