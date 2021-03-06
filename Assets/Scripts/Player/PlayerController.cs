using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	//speeds
	public float speed;
	public float idleSpeed = 5f;
	public float moveSpeed = 10f;
	public float boostSpeed = 15f;
	public float boostFuel = 100f;
	public float boostFuelRate = 5f;
	public float rotationSpeed = 50f;
	public float smoothSpeed = 5f;

	float m_FieldOfView;
	private float rotation;

	private Rigidbody rb;
	public Light nightLight;
	private GameObject cause;

	public GameManager gameManger;

	//Shooting variables
	public float fireRate;
	public GameObject shootObject;
	public GameObject launchLoc;
	private float timeLeft;
	public float minLaunchForce;
	private float currentLaunchForce;

	public float t;
	public float tCool;
	public bool turning;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		speed = idleSpeed;
		boostFuel = 100f;
		m_FieldOfView = 60f;
		currentLaunchForce = minLaunchForce;

		t = 0;
		tCool = 0;
		turning = false;
	}

	void Update () {
		rotation = Input.GetAxisRaw ("Horizontal");

		if (boostFuel > 100)
			boostFuel = 100;

		if (Input.GetKeyDown (KeyCode.F))
			turning = true;

		turnPlane ();

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

		Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, m_FieldOfView, Time.deltaTime * smoothSpeed);
	}

	void FixedUpdate() {
		
		rb.MovePosition (rb.position + transform.forward * speed * Time.deltaTime);
		if (!turning) {	
			Vector3 yRotation = Vector3.up * rotation * (rotationSpeed * (moveSpeed / 10)) * Time.fixedDeltaTime;
			Quaternion deltaRotation = Quaternion.Euler (yRotation);
			Quaternion targetRotation = rb.rotation * deltaRotation;
			rb.MoveRotation (Quaternion.Slerp (rb.rotation, targetRotation, 50f * Time.deltaTime));
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (true);	
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (false);
	}

	public void destroyCause(){
		Destroy (cause);
	}

	public void AddBoost(float amount){
		boostFuel = boostFuel + amount;
	}

	public void turnPlane() {
		if(t < 1 && turning) {
			transform.Rotate (Vector3.up * Time.deltaTime * 180 * (rotation != 0 ? rotation : 1));
			t += Time.deltaTime;
		} else {
			turning = false;
			tCool += Time.deltaTime;
			if (tCool > 2) {
				t = 0;
				tCool = 0;
			}
		}
	}
}