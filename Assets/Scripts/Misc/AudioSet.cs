using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSet : MonoBehaviour {

	void Awake () {
		this.GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("sfxVolume");
	}
}
