using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerAndroid : MonoBehaviour {
	public GameObject playButton;
	public GameObject creditsButton;
	public GameObject settingsButton;
	public GameObject returnButton;
	public GameObject settingsText;

	public GameObject combatButton;
	public GameObject raceButton;

	public GameObject leaderboard;
	public Text combatHighScoreText;
	public Text raceHighScoreText;

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

	void Start() {
		helpNow = false;
		settingsNow = false;
		combatButton.SetActive (false);
		raceButton.SetActive (false);
	}

	public float transitionDuration;
	IEnumerator transition(Vector3 to, bool main, bool play, bool select, int scene) {
		playButton.SetActive (false);
		creditsButton.SetActive (false);
		settingsButton.SetActive (false);
		returnButton.SetActive (false);

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
			settingsText.SetActive (true);
		}

		if (main) {
			playButton.SetActive (true);
			creditsButton.SetActive (true);
			settingsButton.SetActive (true);
		} else if (select) {
			combatButton.SetActive (true);
			raceButton.SetActive (true);
			returnButton.SetActive (true);
		} else if (play) {
			returnButton.SetActive (false);
			combatButton.SetActive (false);
			raceButton.SetActive (false);
			SceneManager.LoadScene (scene);
		} else if (!main) {
			returnButton.SetActive (true);
		} 
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
	}

	public void returnButtonFunction() {
		StartCoroutine (transition (menu, true, false,false, 0));
		leaderboard.SetActive (false);
		helpNow = false;
		helpImg.SetActive (false);
		settingsNow = false;
		settingsText.SetActive (false);
		combatButton.SetActive (false);
		raceButton.SetActive (false);
	}
}

