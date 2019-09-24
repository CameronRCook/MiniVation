using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
	public GameObject target;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null){
			transform.LookAt (target.transform);
		}

		//Locking rotations so the signs don't go into the earth.

		//Commenting out to lock rotations.

		if(this.transform.rotation.x >=90.0f){
			this.transform.rotation.Set (90.0f,this.transform.rotation.y,this.transform.rotation.z, this.transform.rotation.w);
		}
		if(this.transform.rotation.y >=90.0f){
			this.transform.rotation.Set (this.transform.rotation.x,90.0f,this.transform.rotation.z, this.transform.rotation.w);
		}
		if(this.transform.rotation.z >=90.0f){
			this.transform.rotation.Set (this.transform.rotation.x,this.transform.rotation.y,90.0f, this.transform.rotation.w);
		}

		if(this.transform.rotation.x <=-90.0f){
			this.transform.rotation.Set (-90.0f,this.transform.rotation.y,this.transform.rotation.z, this.transform.rotation.w);
		}
		if(this.transform.rotation.y <=-90.0f){
			this.transform.rotation.Set (this.transform.rotation.x,-90.0f,this.transform.rotation.z, this.transform.rotation.w);
		}
		if(this.transform.rotation.z <=-90.0f){
			this.transform.rotation.Set (this.transform.rotation.x,this.transform.rotation.y,-90.0f, this.transform.rotation.w);
		}





		
	}
}
