using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipTilt : MonoBehaviour {
	public Vector3 tmp;
	public PauseManager pauseManager;
	public PlayerController playerController;
	private Scene currentScene;

	public bool menu;

	void Start() {
		currentScene = SceneManager.GetActiveScene ();
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		if(menu) {
			tmp = new Vector3 (Input.GetAxisRaw ("Horizontal") * -25, 0, 0);
			transform.localEulerAngles = tmp;
		} else if(playerController.turning){
			tmp = new Vector3 ((Input.GetAxisRaw("Horizontal") != 0 ? Input.GetAxisRaw("Horizontal") : 1) * -50, 0, 0);
			transform.localEulerAngles = tmp;
		} else if (currentScene.name == "Home" || (!(pauseManager.paused) && !pauseManager.gameOver)) {
			tmp = new Vector3 (Input.GetAxisRaw ("Horizontal") * -25, 0, 0);
			transform.localEulerAngles = tmp;
		}
	}
}