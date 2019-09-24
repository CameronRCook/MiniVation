using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerAndroid : MonoBehaviour{
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

	public Joystick androidJoystick;
	public bool boosting;
    public GameObject miniGunShootCollider;
    public GameObject miniGunEffect;

    void Start() {
		rb = GetComponent<Rigidbody> ();
		speed = idleSpeed;
		boostFuel = 100f;
		m_FieldOfView = 60f;
		currentLaunchForce = minLaunchForce;
		boosting = false;
		t = 0;
		tCool = 0;
		turning = false;
	}

	void Update () {
		//get axis from joystick script.
		rotation = androidJoystick.Horizontal ();

		turnPlane ();

		if (boostFuel > 100)
			boostFuel = 100;

		if (boosting && boostFuel > 0) {
			speed = boostSpeed;
			m_FieldOfView = 70f;
			boostFuel -= (boostFuelRate * Time.deltaTime);
		} else if (androidJoystick.Vertical ()>0) {
			speed = moveSpeed*androidJoystick.Vertical ();
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
		if (other.gameObject.CompareTag ("MiniGun")){
			this.gameObject.GetComponent<ShootingControllerAndroid> ().MiniGun ();
			Destroy (other.gameObject);
            miniGunShootCollider.SetActive(true);
            miniGunEffect.SetActive(true);
		}
			
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

	public void flip() {
		turning = true;
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
	public void Boost(){
		boosting = !boosting;
	}
}