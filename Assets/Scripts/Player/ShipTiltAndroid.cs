using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipTiltAndroid : MonoBehaviour {
	public Vector3 tmp;
	public PauseManager pauseManager;
	public PlayerControllerAndroid playerControllerAndroid;
	private Scene currentScene;
	public Joystick androidJoystick;

	public bool menu;

	void Start() {
		currentScene = SceneManager.GetActiveScene ();
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		if(menu) {
			tmp = new Vector3 (androidJoystick.Horizontal () * -30, 0, 0);
			transform.localEulerAngles = tmp;
		} else if(playerControllerAndroid.turning){
			tmp = new Vector3 ((androidJoystick.Horizontal () != 0 ? androidJoystick.Horizontal (): 1) * -50, 0, 0);
			transform.localEulerAngles = tmp;
		} 
		else if (currentScene.name == "Home" || (!(pauseManager.paused) && !pauseManager.gameOver)) {
			tmp = new Vector3 (androidJoystick.Horizontal () * -30, 0, 0);
			transform.localEulerAngles = tmp;
		}
	
	}
}