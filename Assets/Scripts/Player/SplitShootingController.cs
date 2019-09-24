using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShootingController : MonoBehaviour {
	
	//Shooting variables
	public float fireRate;
	public GameObject shootObject;
	public GameObject launchLoc;
	private float timeLeft;
	public float minLaunchForce;
	private float currentLaunchForce;

	private bool amAndroid;
	public GameObject missleSound;

    public bool playerOne;

	// Use this for initialization
	void Start () {
		currentLaunchForce = minLaunchForce;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if(timeLeft<= 0 && ((playerOne) ? Input.GetKey(KeyCode.Space) : Input.GetKey(KeyCode.Comma)))
        {
			PlayerShoot ();
		}
	}
	//How to fix? I need to look at how I fixed this before I pull.

	public void PlayerShoot(){
		Instantiate (missleSound,this.transform.position,this.transform.rotation);
		Rigidbody shot = Instantiate (shootObject, launchLoc.transform.position, launchLoc.transform.rotation).GetComponent<Rigidbody> ();
		shot.velocity = currentLaunchForce * launchLoc.transform.forward;

		timeLeft = fireRate;
	}

}
