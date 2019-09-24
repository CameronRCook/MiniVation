using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnSphere : MonoBehaviour {
	public Transform planet;
	public Transform parentObject;
	public GameObject tallBuildings;
	public GameObject midBuildings;
	public GameObject shortBuildings;

	private int numTallBuildings;
	public int numTallBuildingsMin = 5;
	public int numTallBuildingsMax = 15;

	private int numMidBuildings;
	public int numMidBuildingsMin = 10;
	public int numMidBuildingsMax = 20;

	private int numShortBuildings;
	public int numShortBuildingsMin = 15;
	public int numShortBuildingsMax = 25;

	private GameObject tallBuilding;
	private GameObject midBuilding;
	private GameObject shortBuilding;

	public float planetRadius;    

	// Use this for initialization
	void Start () {
		StartCoroutine(spawnObjects (numShortBuildings, numShortBuildingsMin, numShortBuildingsMax, shortBuildings, shortBuilding));
		StartCoroutine (spawnObjects (numTallBuildings, numTallBuildingsMin, numTallBuildingsMax, tallBuildings, tallBuilding));
		StartCoroutine (spawnObjects (numMidBuildings, numMidBuildingsMin, numMidBuildingsMax, midBuildings, midBuilding));
	}

	IEnumerator spawnObjects(int num, int min, int max, GameObject structures, GameObject structure/*, bool isObjective*/) {
		num = Random.Range (min, max);
		for (int i = 0; i < num; i++) {
			Vector3 a = Random.onUnitSphere * planetRadius;

			structure = Instantiate (structures,a, Quaternion.identity, parent:parentObject);
			structure.transform.LookAt (planet);

			Vector3 euler = structure.transform.eulerAngles;
			euler.z = Random.Range(0f, 360f);
			structure.transform.eulerAngles = euler;
		}

		yield return true;
	}
}
