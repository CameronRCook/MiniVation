using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {

	public static FauxGravityAttractor instance;

	private SphereCollider col;
	public bool push;

	void Awake () {
		instance = this;
		col = GetComponent<SphereCollider> ();
	}

	public float gravity = -10f;

	public void Attract (Rigidbody body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		body.AddForce (gravityUp * gravity);
		if(push){
			body.AddForce (gravityUp * 10);
			Debug.Log ("Gravity if");
		}
		Debug.Log (push);

		RotateBody (body);

	}

	public void PlaceOnSurface (Rigidbody body) {
		body.MovePosition ((body.position - transform.position).normalized * (transform.localScale.x * col.radius));
		if(push){
			Vector3 gravityUp = (body.position - transform.position).normalized;
			body.AddForce (gravityUp * 10);
			Debug.Log ("Gravity if");
		}
		RotateBody (body);
	}

	void RotateBody (Rigidbody body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Quaternion targetRotation = Quaternion.FromToRotation (body.transform.up, gravityUp) * body.rotation;
		body.MoveRotation (Quaternion.Slerp (body.rotation, targetRotation, 50f * Time.deltaTime));
	}


}
