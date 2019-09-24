using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shot : MonoBehaviour {
	Rigidbody rb;
	public CameraShake cameraShake;
	public bool shake;

	//Small explosion
	public GameObject smallExplosionEffect;
	private bool doShake;
	public float timeToDeath;
	private float currentTime;
	private bool amAndroid;
	private Vector3 startVelocity;

	public GameObject boomShotAudio;

	private bool doVibrate;


	void Start () {
		if(SceneManager.GetActiveScene ().name == "MainAndroid" ||SceneManager.GetActiveScene ().name == "Race_Game_Android"){
			amAndroid = true;
		}
		else{
			amAndroid = false;
		}
		rb = this.GetComponent<Rigidbody> ();
		cameraShake = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();

        if (this.gameObject.name != "MiniGunCollider")
        {
            startVelocity = rb.velocity;
            currentTime = timeToDeath;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.name != "MiniGunCollider")
        {
            currentTime -= Time.deltaTime;
            rb.velocity = startVelocity;
            if (currentTime <= 0)
            {
                Destroy(this.gameObject);
            }
        }

	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag ("Shot")){
			Instantiate (smallExplosionEffect,this.transform.position,this.transform.rotation);
			Instantiate (boomShotAudio,this.transform.position,this.transform.rotation);
			cameraShake.shake = true;
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
		//uncomment for android build.
		if((other.gameObject.CompareTag("Shot") || other.gameObject.CompareTag("Enemy"))) {
			if(PlayerPrefs.GetInt ("ExplosionVibrate")==0){
				Handheld.Vibrate ();
				Debug.Log ("Vibrating");
			}
		}
	}

}

