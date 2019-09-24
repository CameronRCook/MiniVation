using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMenu : MonoBehaviour {
	private Rigidbody rb;
	private float rotation;

	public float rotationSpeed = 50f;
	public float speed;
	public Light nightLight;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		rotation = Input.GetAxisRaw ("Horizontal");
	}

	void FixedUpdate() {
		rb.MovePosition (rb.position + transform.forward * speed * Time.deltaTime);
		Vector3 yRotation = Vector3.up * rotation * (rotationSpeed) * Time.fixedDeltaTime;
		Quaternion deltaRotation = Quaternion.Euler (yRotation);
		Quaternion targetRotation = rb.rotation * deltaRotation;
		rb.MoveRotation (Quaternion.Slerp (rb.rotation, targetRotation, 50f * Time.deltaTime));
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (false);
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (true);	
	}
}
