using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerDepricated : MonoBehaviour {
	private List<string> keys = new List<string>{"a", "d", "e", "i", "j", "k", "l", "o", "u", "w"};
	List<string> finalKeys;

	//Slots for the random UI spot.
	public Image firstSlot;
	public Image secondSlot;
	public Image thirdSlot;
	public Image fourthSlot;
	public Image fifthSlot;

	//Different Key Textures.
	public Texture aKey;
	public Texture dKey;
	public Texture eKey;
	public Texture iKey;
	public Texture jKey;
	public Texture kKey;
	public Texture lKey;
	public Texture oKey;
	public Texture uKey;
	public Texture wKey;

	private bool quizzing;

	void Start() {
		//Making all the UI inactive.
		firstSlot.gameObject.SetActive (false);
		secondSlot.gameObject.SetActive (false);
		thirdSlot.gameObject.SetActive (false);
		fourthSlot.gameObject.SetActive (false);
		fifthSlot.gameObject.SetActive (false);
		quizzing = false;
	finalKeys = new List<string> ();
	}

	void FixedUpdate() {
		//This if statement only runs if the player is not being quizzed with the random keys.
		if (quizzing) {
			//What do if not quizzing
		}
	}
		
	void OnTriggerEnter(Collider other){
		//Checks to see if it hit an action zone.
		if(other.gameObject.CompareTag ("ActionZone")){
			randomKeyString();
		}


	}

	public void randomKeyString(){

		//Stops the player.
	//	quizzing = true;

		for(int i = 0; i < 5; i++){
			int ranK = Random.Range (0, 9);
			finalKeys.Add (keys[ranK]);
		}
		//Making a list to store textures we will then heave them into the key images.
		List<Texture> textures = new List<Texture>();
		for(int j = 0; j< finalKeys.Count;j++){
			if(finalKeys[j] == "a"){
				textures.Add (aKey);
			}
			else if(finalKeys[j] == "d"){
				textures.Add (dKey);
			}
			else if(finalKeys[j] == "e"){
				textures.Add (eKey);
			}
			else if(finalKeys[j] == "i"){
				textures.Add (iKey);
			}
			else if(finalKeys[j] == "j"){
				textures.Add (jKey);
			}
			else if(finalKeys[j] == "k"){
				textures.Add (kKey);
			}
			else if(finalKeys[j] == "l"){
				textures.Add (lKey);
			}
			else if(finalKeys[j] == "o"){
				textures.Add (oKey);
			}
			else if(finalKeys[j] == "u"){
				textures.Add (uKey);
			}
			else if(finalKeys[j] == "w"){
				textures.Add (wKey);
			}
		}

		//Setting slots to the random keys.
		firstSlot.canvasRenderer.SetTexture (textures[0]);
		secondSlot.canvasRenderer.SetTexture (textures[1]);
		thirdSlot.canvasRenderer.SetTexture (textures[2]);
		fourthSlot.canvasRenderer.SetTexture (textures[3]);
		fifthSlot.canvasRenderer.SetTexture (textures[4]);

		firstSlot.gameObject.SetActive (true);
		secondSlot.gameObject.SetActive (true);
		thirdSlot.gameObject.SetActive (true);
		fourthSlot.gameObject.SetActive (true);
		fifthSlot.gameObject.SetActive (true);

		int stage = 0;
		//while(quizzing){
		Debug.Log ("IM HERE");
			if(Input.GetKeyDown (finalKeys[stage])){
				Debug.Log ("hit key");
				stage++;
			}
			if(Input.anyKeyDown && !Input.GetKeyDown (finalKeys[stage])){
				stage = 0;
				Debug.Log ("hit wrong key");
			}
		if(stage >=4){
			quizzing = false;
		}
	//	}
		quizTime ();



	}

	private void quizTime(){
		int stage = 0;
		while(quizzing){
			Debug.Log ("Inside quizTime");
			if(Input.GetKeyDown (finalKeys[stage])){
				Debug.Log ("hit key");
				stage++;
			}
			if(Input.anyKeyDown && !Input.GetKeyDown (finalKeys[stage])){
				stage = 0;
				Debug.Log ("hit wrong key");
			}
		}
	}



}
