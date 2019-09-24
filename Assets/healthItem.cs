using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthItem : MonoBehaviour {
	public float rotSpeed;
	public PlayerManager playerManager;
	public float healAmount;

	void Start() {
		playerManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerManager> ();
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.right * Time.deltaTime * rotSpeed);
		transform.Rotate (Vector3.up * Time.deltaTime * rotSpeed);	
	}

	void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag("Player") && playerManager.playerHealth < 25) {
            if (playerManager.playerHealth >= 15) {
                playerManager.playerHealth = 25;
            } else {
                playerManager.playerHealth += healAmount;
            }
            playerManager.updateHealth();
            Destroy(this.gameObject);
        }
	}
}
