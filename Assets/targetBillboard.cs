using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBillboard : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3 (this.transform.position.x, this.transform.position.y, target.transform.position.z);
		this.transform.LookAt (targetPosition);		
	}
}
