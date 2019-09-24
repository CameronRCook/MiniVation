using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
	public float playerHealth;
	public Text playerHealthText;
	public GameManager gameManager;
	public PauseManager pauseManager;

	public Image playerHealthBar;
	public GameObject smallPlayerExplosion;
	public GameObject largePlayerExplosion;

	public GameObject planeBody;
	private CameraShake cameraShake;

	// Use this for initialization
	void Start () {
		cameraShake = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag ("Shot")){
			LoseHealth (5.0f);
			Instantiate (smallPlayerExplosion,this.transform.position,this.transform.rotation);
			cameraShake.shake = true;
			Destroy (other.gameObject);
			if(playerHealth<=0){
				Destroy (planeBody);
				gameManager.GameOver ();
			}
		}
		else if (other.gameObject.CompareTag ("Enemy")){
            Debug.Log ("Game Over");
			cameraShake.shake = true;
			Destroy (planeBody);
			Instantiate (largePlayerExplosion,this.transform.position,this.transform.rotation);
			gameManager.GameOver ();
		}

		playerHealthBar.fillAmount = playerHealth / 25;	
	}

    public void updateHealth()
    {
        playerHealthBar.fillAmount = playerHealth / 25;
    }

	public void LoseHealth(float amount){
		playerHealth -= amount;

		playerHealthBar.fillAmount = playerHealth / 25;	

		//Debug.Log ("Player health: " + playerHealth);
		/*	playerHealthText.text = "Player Health: " + playerHealth;
	*/	if(playerHealth<=0){
			pauseManager.gameOver = true;
		}
	}
	void Update(){
		if(playerHealth<=0){
			pauseManager.gameOver = true;
		}
	}
}