using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FauxGravityBodyTest : MonoBehaviour {

	private FauxGravityAttractorTest attractor;
	private Rigidbody rb;

	public bool placeOnSurface = false;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		attractor = FauxGravityAttractorTest.instance;
	}

	void FixedUpdate() {
		if (placeOnSurface)
			attractor.PlaceOnSurface (rb);
		else
			attractor.Attract (rb);
	}
}
