using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotation : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (Vector3.right * 1000f * Time.deltaTime, Space.Self);
	}
}
