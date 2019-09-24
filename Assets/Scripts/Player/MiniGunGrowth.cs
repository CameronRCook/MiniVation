using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGunGrowth : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.localScale = new Vector3(5f, 3.77f, 7f);
	}
	
	// Update is called once per frame
	void Update () {
	    if(this.transform.localScale.y < 70)
        {
            this.transform.localScale = new Vector3(5, this.transform.localScale.y + 100, 7);
        }
        
        
	}
}
