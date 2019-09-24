using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boostItem : MonoBehaviour {
	public float rotSpeed;
	public PlayerController playerController;
	public float boostRefuel;

	public PlayerControllerAndroid playerControllerAndroid;
	private bool amAndroid;

	// Use this for initialization
	void Start () {
		if(SceneManager.GetActiveScene ().name == "MainAndroid" ||SceneManager.GetActiveScene ().name == "Race_Game_Android" ){
			playerControllerAndroid = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerAndroid> ();
			amAndroid = true;
		} else {
			playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();	
			amAndroid = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.right * Time.deltaTime * rotSpeed);
		transform.Rotate (Vector3.up * Time.deltaTime * rotSpeed);	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
            if (!amAndroid && playerController.boostFuel != 100)
            {
                if (playerController.boostFuel >= 90)
                {
                    playerController.boostFuel = 100;
                }
                else
                {
                    playerController.boostFuel += boostRefuel;
                }
                Destroy(this.gameObject);
            }
            else if (playerControllerAndroid.boostFuel != 100)
            {
                if (playerControllerAndroid.boostFuel >= 90)
                {
                    playerControllerAndroid.boostFuel = 100;
                }
                else
                {
                    playerControllerAndroid.boostFuel += boostRefuel;
                }
                Destroy(this.gameObject);
            }
		}
	}
}
