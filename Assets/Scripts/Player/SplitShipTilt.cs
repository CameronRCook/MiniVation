using UnityEngine;
using UnityEngine.SceneManagement;

public class SplitShipTilt : MonoBehaviour {
	public Vector3 tmp;
	public PauseManager pauseManager;
	public SplitPlayerController splitPlayerController;
	private Scene currentScene;

	public bool menu;

	void Start() {
		currentScene = SceneManager.GetActiveScene ();
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		if(menu) {
			tmp = new Vector3 (Input.GetAxisRaw((splitPlayerController.playerOne) ? "Horizontal1" : "Horizontal2") * -25, 0, 0);
			transform.localEulerAngles = tmp;
		} else if(splitPlayerController.turning){
			tmp = new Vector3 ((Input.GetAxisRaw((splitPlayerController.playerOne) ? "Horizontal1" : "Horizontal2") != 0 ? Input.GetAxisRaw((splitPlayerController.playerOne) ? "Horizontal1" : "Horizontal2") : 1) * -50, 0, 0);
			transform.localEulerAngles = tmp;
		} else if (currentScene.name == "Home" || (!(pauseManager.paused) && !pauseManager.gameOver)) {
			tmp = new Vector3 (Input.GetAxisRaw((splitPlayerController.playerOne) ? "Horizontal1" : "Horizontal2") * -25, 0, 0);
			transform.localEulerAngles = tmp;
		}
	}
}