using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElevationScript : MonoBehaviour {

	public GameObject player;
	public GameObject planetGeom;
	public GameObject planet;

	// Update is called once per frame

	void OnTriggerEnter(Collider a){
		if(a.gameObject.CompareTag ("PlanetGeometry")){
	
			Vector3 direction = a.gameObject.transform.position - player.transform.position;
			direction.Normalize ();
			planet.GetComponent<FauxGravityAttractor> ().push= true;
		}
		else{
			planet.GetComponent<FauxGravityAttractor> ().push = false;
		}
	}
	void Update(){
		
	}
}
