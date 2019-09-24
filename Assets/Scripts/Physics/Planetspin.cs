using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetspin : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		this.transform.Rotate (Vector3.right * 2f * Time.deltaTime);

	}
}
