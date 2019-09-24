using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingControllerAndroid : MonoBehaviour {

	//Shooting variables
	public float fireRate;
	public GameObject shootObject;
	public GameObject launchLoc;
	private float timeLeft;
	public float minLaunchForce;
	private float currentLaunchForce;

	private bool amAndroid;
	public GameObject missleSound;

	//minigun variables
	public float miniGunFireRate;
	public float miniGunTimeLeft;
	public bool isMiniGun;
	public GameObject miniGunBullet;
	public bool shootingMini;



	// Use this for initialization
	void Start () {
		currentLaunchForce = minLaunchForce;
		isMiniGun = false;
		shootingMini = false;
	}

	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		//This checks if the player is in a minigun session and counts down the time left in the powerup.
		//If it runs out, the fire rate is set back to normal.
		if(isMiniGun){
			miniGunTimeLeft -= Time.deltaTime;
			if(miniGunTimeLeft<=0){
				Debug.Log ("over");
				isMiniGun = false;
			}
		}
		if(shootingMini){
			Rigidbody shot = Instantiate (miniGunBullet, launchLoc.transform.position, launchLoc.transform.rotation).GetComponent<Rigidbody> ();
			shot.velocity = currentLaunchForce * launchLoc.transform.forward;
		}
	}

	public void PlayerShoot(){
		if (timeLeft <= 0) {
			Instantiate (missleSound,this.transform.position,this.transform.rotation);
			/*if(isMiniGun){
				Rigidbody shot = Instantiate (miniGunBullet, launchLoc.transform.position, launchLoc.transform.rotation).GetComponent<Rigidbody> ();
				shot.velocity = currentLaunchForce * launchLoc.transform.forward;
				timeLeft = miniGunFireRate;
			}
			else{
*/
				Rigidbody shot = Instantiate (shootObject, launchLoc.transform.position, launchLoc.transform.rotation).GetComponent<Rigidbody> ();
				shot.velocity = currentLaunchForce * launchLoc.transform.forward;
				timeLeft = fireRate;	
//			}
		}
	}


	public void MiniGun(){
		isMiniGun = true;
		timeLeft = miniGunFireRate;
	}

	public void ReleasedFireButton(){
		shootingMini = false;
		Debug.Log ("released");
	}
	public void PressedFireButton(){
		if(isMiniGun){
			shootingMini = true;		
		}
	}
}
