using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
	public bool shake;

	void Update() {
		if(shake) {
			StartCoroutine (Shake (.15f, .4f));
		}
	}

	public IEnumerator Shake (float duration, float magnitude) {
		shake = false;

		Vector3 originalPos = transform.localPosition;

		float elapsed = 0.0f;

		while (elapsed < duration) {
			float x = Random.Range (-1f, 1f) * magnitude;
			float y = Random.Range (-1f, 1f) * magnitude;

			transform.localPosition = new Vector3 (x, y, originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}


		transform.localPosition = originalPos;
	}
}
