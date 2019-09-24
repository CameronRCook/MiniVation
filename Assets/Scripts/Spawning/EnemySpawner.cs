using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;
	public float spawnRate;
	public int maxEnemiesPerSession;
	public float timeBetweenSessions;

	private float currentSessionTime;
	private float currentT;
	private int spawnedSessionCount;

	public GameManager gameManager;

	// Use this for initialization
	void Start () {
		currentT = spawnRate;
		currentSessionTime = timeBetweenSessions;
		spawnedSessionCount = maxEnemiesPerSession;
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.start) {
			if (currentSessionTime > 0) {
				currentSessionTime -= Time.deltaTime;

			}
			if (currentSessionTime <= 0) {
				SpawnAlgorithm ();
			}
		}
	}


	private void SpawnAlgorithm(){
		if (spawnedSessionCount > 0) {
			currentT -= Time.deltaTime;
			if (currentT <= 0) {
				Instantiate (enemy,this.transform.position,new Quaternion(0,this.transform.rotation.y,this.transform.rotation.z, this.transform.rotation.w));
				currentT = spawnRate;
				spawnedSessionCount-=1;
			}
		}
		else{
			spawnedSessionCount = maxEnemiesPerSession;
			currentT = spawnRate;
			currentSessionTime = timeBetweenSessions;
		}
	}

}
