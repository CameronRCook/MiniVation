using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerSimplify : MonoBehaviour {
	public float speed;
	public float idleSpeed = 5f;
	public float moveSpeed = 10f;
	public float boostSpeed = 15f;
	public float boostFuel = 100f;
	public float boostFuelRate = 5f;
	public float rotationSpeed = 10f;

	float m_FieldOfView;

	private float rotation;
	private Rigidbody rb;

	public Light nightLight;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		speed = idleSpeed;
		boostFuel = 100f;
		m_FieldOfView = 60f;
	}

	void Update () {
		rotation = Input.GetAxisRaw ("Horizontal");

		if (Input.GetKey (KeyCode.LeftShift) && boostFuel > 0) {
			speed = boostSpeed;
			m_FieldOfView = 70f;
			boostFuel -= (boostFuelRate * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.W)) {
			speed = moveSpeed;
			m_FieldOfView = 65f;
		} else {
			speed = idleSpeed;
			m_FieldOfView = 60f;
		}
		Camera.main.fieldOfView = m_FieldOfView;
	}

	void FixedUpdate() {
		rb.MovePosition (rb.position + transform.forward * speed * Time.deltaTime);
		Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
		Quaternion deltaRotation = Quaternion.Euler (yRotation);
		Quaternion targetRotation = rb.rotation * deltaRotation;
		rb.MoveRotation (Quaternion.Slerp (rb.rotation, targetRotation, 50f * Time.deltaTime));
	}
		
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (true);	
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (false);
	}
}
