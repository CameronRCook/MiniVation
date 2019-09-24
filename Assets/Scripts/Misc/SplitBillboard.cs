using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBillboard : MonoBehaviour {
    // Update is called once per frame
    public GameObject targetCamera;

	void LateUpdate () {
		this.transform.LookAt (targetCamera.transform.position, -Vector3.up);
	}
}
