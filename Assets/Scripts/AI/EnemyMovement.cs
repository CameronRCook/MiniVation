using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour {
	private GameObject target;
	public float moveRate;
	public float fireRate;
	public GameObject shootObject;
	public GameObject launchLoc;
	private float timeLeft;

	public float minLaunchForce;
	private float currentLaunchForce;

	private float keepDistance;
	private float currentDistance;

	//New variables.
	private Rigidbody rb;
	private float rotation;
	public float speed;

	public float rotationSpeed = 50f;
	public float moveSpeed = 10f;
	public GameObject nightLight;

	//Explosion
	public GameObject explosionEffect;

	//Powerups and drops
	public GameObject boostItem;
	public int boost;
	public GameObject healthItem;
	public GameObject miniGunItem;



	public Transform planet;

	//New variables
	private GameManager manager;
	private bool amAndroid;
	public GameObject fieldOfViewObject;
	private FieldOfView fieldOfView;

	private float initalStartWait;
	private float currentInitalStartWait;
	private bool startNow;

	private CameraShake cameraShake;
	public GameObject missleSound;
	public GameObject explosionSound;

	void Start () {
		cameraShake = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();
		//healthItem = GameObject.FindGameObjectWithTag ("health");
		startNow = false;
		initalStartWait = 5.0f;
		currentInitalStartWait = initalStartWait;
		startNow = true;
		fieldOfView = fieldOfViewObject.GetComponent<FieldOfView> ();
		if(SceneManager.GetActiveScene ().name == "MainAndroid"||SceneManager.GetActiveScene ().name == "Race_Game_Android"){
			amAndroid = true;
		}
		else{
			amAndroid = false;
		}
		rb = this.GetComponent<Rigidbody> ();
		timeLeft = fireRate;
		target = GameObject.FindGameObjectsWithTag ("Player")[0];
		currentLaunchForce = minLaunchForce;
		keepDistance = Vector3.Distance (this.transform.position, new Vector3 (0, 0, 0));
		speed = 7f;

		boost = (Random.Range (1, 3));

		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void Update () {
		currentInitalStartWait -= Time.deltaTime;
		if(currentInitalStartWait<=0){
			startNow = true;
		}
		timeLeft -= Time.deltaTime;
		if (startNow) {
			if (timeLeft <= 0 && fieldOfView.inSight) {
				shoot ();
			}
		}
		//This is the direction. We add a collider to the bottom of the enemy, seperate object.
		Vector3 oppositeDirection = new Vector3(0.0f+this.transform.position.x,0.0f+this.transform.position.y,0.0f+this.transform.position.z);
		this.transform.LookAt (target.transform, oppositeDirection);
	
		//Check rotations.
		if(this.transform.rotation.x >80){
			this.transform.rotation = new Quaternion (80, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
		}
		if(this.transform.rotation.y >80){
			this.transform.rotation = new Quaternion (this.transform.rotation.x, 80, this.transform.rotation.z, this.transform.rotation.w);
		}
		if(this.transform.rotation.z >80){
			this.transform.rotation = new Quaternion (this.transform.rotation.x, this.transform.rotation.y, 80, this.transform.rotation.w);
		}
	}

	void LateUpdate(){
		currentDistance=Vector3.Distance (this.transform.position, new Vector3 (0, 0, 0));
		if(currentDistance != keepDistance){
			//The last portion may need to calculate each individual x y z.
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (0, 0, 0), (currentDistance - keepDistance));
		}

	}

	 void FixedUpdate() {
		rb.MovePosition (rb.position + transform.forward * speed * Time.deltaTime);
		//Vector3 yRotation = Vector3.up * rotation * (rotationSpeed * (moveSpeed / 10)) * Time.fixedDeltaTime;
		Vector3 yRotation = Vector3.up * (rotationSpeed * (moveSpeed / 10)) * Time.fixedDeltaTime;
		Quaternion deltaRotation = Quaternion.Euler (yRotation);
		Quaternion targetRotation = rb.rotation * deltaRotation;
		rb.MoveRotation (Quaternion.Slerp (rb.rotation, targetRotation, 50f * Time.deltaTime));
	}

	public void shoot(){
			Instantiate (missleSound, this.transform.position, this.transform.rotation);
			//Instantiate a new object and apply a force to it.
			Rigidbody shot = Instantiate (shootObject, launchLoc.transform.position, launchLoc.transform.rotation).GetComponent<Rigidbody> ();
			shot.velocity = currentLaunchForce * launchLoc.transform.forward;
			//Reset the timer.
			timeLeft = fireRate;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Shot"){
			spawnItem ();
			Destroy (other.gameObject);
			manager.IncreaseScore (1);
			cameraShake.shake = true;
			if(!amAndroid){
				target.GetComponent<PlayerController> ().AddBoost (10f);
			}
			else if(amAndroid){
				target.GetComponent<PlayerControllerAndroid> ().AddBoost (10f);

			}
			Instantiate (explosionSound,this.transform.position,this.transform.rotation);
			Instantiate (explosionEffect, this.transform.position,this.transform.rotation);
			Destroy (this.gameObject);
		}
      /*  if(other.gameObject.tag=="MiniGunColl")
        {
            Instantiate(explosionSound, this.transform.position, this.transform.rotation);
            Instantiate(explosionEffect, this.transform.position, this.transform.rotation);
        }
	*/	
    if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (true);	
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("darkSide"))
			nightLight.gameObject.SetActive (false);
	}

	public void spawnItem() {
		int num = Random.Range (1,4);

		Debug.Log (num);

		if (num == 1)
			Instantiate (boostItem, this.transform.position, this.transform.rotation);
		else if (num == 2)
			Instantiate (healthItem, this.transform.position, this.transform.rotation);
	/*	else if (num ==3)
			Instantiate (miniGunItem,this.transform.position, this.transform.rotation);
            
		*/else
			return;
	}

}
