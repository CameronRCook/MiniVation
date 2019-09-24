using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
	public float transitionDuration;
	public Camera mainCamera;

	public void combatSelect() {
		StartCoroutine (transition (2));
	}

	public void raceSelect() {
		StartCoroutine (transition (3));
	}

	IEnumerator transition(int Scene) {
		float t = 0.0f;
		while (t < 1.0) {
			mainCamera.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 
				150f, Time.deltaTime * 5);
			t += Time.deltaTime * (Time.timeScale / transitionDuration);
			yield return 0;
		}
		Time.timeScale = 0;
		SceneManager.LoadScene (Scene);
	}
}
