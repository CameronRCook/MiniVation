using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectivesOnSphere : MonoBehaviour {
	//Current allowed max numer of objectives per wave
	public int maxObjectives;		

	//HARD max. No more than 5 spawns per wave;
	public int maxMaxObjectives;
	public int minObjectives;

	//These rates are in seconds. Starting at 45 seconds, and doing quick-maths until it reaches a min of 15 second waves.
	public float spawnRate;
	public float minSpawnRate;

	//Various stats for High-Tier Objectives
	public GameObject objectiveHigh;
	public float highDamage;
	public float highRepair;
	public int currentHigh;

	//Various stats for Mid-Tier Objectives
	public GameObject objectiveMid;
	public float midDamage;
	public float midRepair;
	public int currentMid;

	//Various stats for Low-Tier Objectives
	public GameObject objectiveLow;
	public float lowDamage;
	public float lowRepair;
	public int currentLow;

	//Total num of objectives (all kinds) on the world's surface
	public int currentTotal;

	public float planetRadius;

	//Total num of objectives spawned this wave
	private int currentWave = 1;

	//Current wavecount
	public int waveCount = 0;

	//Pretty self explainatory, in seconds.
	public float timeSinceLastWave = 0;
	public Transform parentObject;
	public Transform planet;
	public GameObject warningIcons;

	//Whether or not the first objective has been solved.
	public static bool firstObjective = true;

	public float damageAmount;

	public Text Warnings;
	public GameObject Warning;

	// Initialize the world, and spawn first objective
	void Start () {
		maxObjectives = 1;
		minObjectives = 1;
		StartCoroutine (spawnObjectives (objectiveLow));
		currentLow++;
		waveCount++;
		currentTotal = currentLow + currentMid + currentHigh;
		StartCoroutine (giveWarnings ());
		firstObjective = false; //SET THIS TO TRUE, was false for testing
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastWave += (1 * Time.deltaTime);

		//Determines the total amount of damage, perscond to be caused.
		// Range 0 - 1, 1 being KO
		damageAmount = ((currentLow * (lowDamage/1000)) + (currentMid * (midDamage/1000)) + (currentHigh * (highDamage/1000)));

		//Makes sure the spawn rate is held at a min, there is probably a better way to do this.
		if(spawnRate < minSpawnRate) {
			spawnRate = minSpawnRate;
		}

		/*	This is where the magic happens. The number to be spawned this wave is determined, between 1 - the maxObjectives.
		 * 	The clock since lastWave begins counting down. The spawning progress begins, for each iteration, it randomly determines
		 *  what tier the objective will be. Percentages break down: LowTier - 50% MidTier - 37.5% HighTier - 12.25%. 
		 * 	Adds 1 to count of whatever tier. Adjusts spawn rate & wavecount.
		 */
		if (firstObjective == false && (timeSinceLastWave / spawnRate >= 1f)) {
			currentWave = Random.Range (minObjectives, maxObjectives);
			timeSinceLastWave = 0f;
			for (int i = 0; i < currentWave; i++) {
				int tierSpawn = Random.Range (1, 96);
				if(tierSpawn <= 48) {
					StartCoroutine (spawnObjectives (objectiveLow));
					currentLow++;
				} else if(tierSpawn > 48 && tierSpawn <= 84) {
					StartCoroutine (spawnObjectives (objectiveMid));
					currentMid++;
				} else if(tierSpawn > 84 && tierSpawn <= 96) {
					StartCoroutine (spawnObjectives (objectiveHigh));
					currentHigh++;
				}
			}
			waveCount++;

			//Change SpawnRate
			spawnRate -= Mathf.Pow(1.2f, waveCount);

			if (maxObjectives <= maxMaxObjectives) {
				maxObjectives += (int)(waveCount / 2.5f);
			} else {
				maxObjectives = maxMaxObjectives;
			}

			if(waveCount >= 10) {
				minObjectives = 2;
			}
		}

		currentTotal = currentLow + currentMid + currentHigh;

		if(currentTotal > 0) {
			warningIcons.SetActive (true);
			Warnings.text = "WARNING: Disasters Emerge! \n " +
				"Disasters: \n" +
				"\t- " + currentLow + " Low Tier. \n " +
				"\t- " + currentMid + " Mid Tier. \n " +
				"\t- " +currentHigh + " High Tier. \n " +
				"Total Diasters: " + currentTotal + ".";
		} else if (currentTotal == 0) {
			warningIcons.SetActive (false);
			Warnings.text = "CONGRATS: No Diasasters Present! \n " +
			"Disasters Imminent: \n" +
				"\t T-: " + (int)(spawnRate - timeSinceLastWave) + " Seconds. \n " +
			"Be Alert! ";
		}
	}

	IEnumerator giveWarnings() {
		
		yield return new WaitForSeconds (10);
	}

	//New Crisp Clean function baby!
	IEnumerator spawnObjectives(GameObject objective) {
			//Storing in a Vector3 now in order to use for later.
			Vector3 a = Random.onUnitSphere * planetRadius;

			objective = Instantiate (objective, a, Quaternion.identity, parent:parentObject);
			objective.transform.LookAt (planet);

		yield return true;
	}
}
