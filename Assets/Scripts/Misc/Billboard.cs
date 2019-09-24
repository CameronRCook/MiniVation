using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	// Update is called once per frame

	void LateUpdate () {
		this.transform.LookAt (Camera.main.transform.position, -Vector3.up);
	}
}
