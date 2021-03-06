using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour {
	
	private List<string> keys= new List<string>(); 
//	private List<string> keys;

	List<string> finalKeys = new List<string>();
	List<Texture> textures = new List<Texture>();
	//Slots for the random UI spot.
	public Image firstSlot;
	public Image secondSlot;
	public Image thirdSlot;
	public Image fourthSlot;
	public Image fifthSlot;

	private List<Image> slots = new List<Image>();
	private Color doneC;
	//Different Key Textures.

	public Texture iKey;
	public Texture jKey;
	public Texture kKey;
	public Texture lKey;
	public Texture oKey;
	public Texture uKey;
	//public Texture wKey;

	public bool quizzing;
	private int stage;
	public bool finishedQuiz;

	//The amount of additional boost given to the player.
	public float boostBoost;

	private bool firstTime;


	public GameManager gameManager;

	void Awake()
	{
		finalKeys = new List<string> ();
	
		keys.Add ("I");
		keys.Add ("J");
		keys.Add ("K");
		keys.Add ("L");
		keys.Add ("O");
		keys.Add ("U");
		stage = 0;
		firstTime = true;



		slots.Add (firstSlot);
		slots.Add (secondSlot);
		slots.Add (thirdSlot);
		slots.Add (fourthSlot);
		slots.Add (fifthSlot);
		doneC = new Color (0.0f, 1.0f, 0.0f, 0.5f);
	}

	void Start() {
		//Making all the UI inactive.
		quizzing = false;

	}

	void FixedUpdate() {
		//This if statement only runs if the player is not being quizzed with the random keys.
		if(finishedQuiz){
			displayKeys (false);
		}
		if (quizzing) {
			firstSlot.canvasRenderer.SetTexture (textures [0]);
		}
	}
	public void displayKeys(bool swith){
		firstSlot.gameObject.SetActive (swith);
		secondSlot.gameObject.SetActive (swith);
		thirdSlot.gameObject.SetActive (swith);
		fourthSlot.gameObject.SetActive (swith);
		fifthSlot.gameObject.SetActive (swith);
	}

	public void randomKeyString(){

		finishedQuiz = false;

		//Stops the player.
		quizzing = true;
		displayKeys (true);

	
		for(int i = 0; i < 5; i++){
			int ranK = Random.Range (0, 5);

			finalKeys.Add (keys[ranK]);
		}
		Debug.Log ("Started");
		//Making a list to store textures we will then heave them into the key images.

		for(int j = 0; j< finalKeys.Count;j++){
			
			if(finalKeys[j] == "I"){
				Debug.Log ("finalKeys[" + j + "] is I");
				textures.Add (iKey);
			}
			else if(finalKeys[j] == "J"){
				Debug.Log ("finalKeys[" + j + "] is J");
				textures.Add (jKey);
			}
			else if(finalKeys[j] == "K"){
				Debug.Log ("finalKeys[" + j + "] is K");
				textures.Add (kKey);
			}
			else if(finalKeys[j] == "L"){
				Debug.Log ("finalKeys[" + j + "] is L");
				textures.Add (lKey);
			}
			else if(finalKeys[j] == "O"){
				Debug.Log ("finalKeys[" + j + "] is O");
				textures.Add (oKey);
			}
			else if(finalKeys[j] == "U"){
				Debug.Log ("finalKeys[" + j + "] is U");
				textures.Add (uKey);
			}

		}
		for(int i = 0; i < finalKeys.Count;i++){
			Debug.Log ("At index " + i + " the value is " + finalKeys [i]);
		}

		if(textures[0] != null){

			Debug.Log ("there is something here.");
		}

		//Debug.Log (textures [0].name);
		//Setting slots to the random keys.
		firstSlot.canvasRenderer.SetTexture (textures[0]);
		secondSlot.canvasRenderer.SetTexture (textures[1]);
		thirdSlot.canvasRenderer.SetTexture (textures[2]);
		fourthSlot.canvasRenderer.SetTexture (textures[3]);
		fifthSlot.canvasRenderer.SetTexture (textures[4]);


		stage = 0;
		quizTime ();
		

	}

	public void quizTime(){
		string key = finalKeys [stage];
		//time for tedious work babe.
		bool keyHit = false;
	
//		Debug.Log (textures [0].ToString ());
		firstSlot.canvasRenderer.SetTexture (textures[0]);
		secondSlot.canvasRenderer.SetTexture (textures[1]);
		thirdSlot.canvasRenderer.SetTexture (textures[2]);
		fourthSlot.canvasRenderer.SetTexture (textures[3]);
		fifthSlot.canvasRenderer.SetTexture (textures[4]);


			if (key == "I") {
				if (Input.GetKeyDown (KeyCode.I)) {
				slots [stage].color = doneC;
					key = finalKeys [stage];
					keyHit = true;
					Debug.Log ("Stage "+stage);
					stage++;
				}
			} 
			else if (key == "J") {
				if (Input.GetKeyDown (KeyCode.J)) {
					slots [stage].color = doneC;
					key = finalKeys [stage];
					keyHit = true;
					Debug.Log ("Stage "+stage);
					stage++;
				}
			} 
			else if (key == "K") {
				if (Input.GetKeyDown (KeyCode.K)) {
					slots [stage].color = doneC;
					key = finalKeys [stage];
					keyHit = true;
					Debug.Log ("Stage "+stage);
					stage++;
				}
			} 
			else if (key == "L") {
				if (Input.GetKeyDown (KeyCode.L)) {
					slots [stage].color = doneC;
					key = finalKeys [stage];
					keyHit = true;
					Debug.Log ("Stage "+stage);
					stage++;
				}
			} 
			else if (key == "O") {
				if (Input.GetKeyDown (KeyCode.O)) {
					slots [stage].color = doneC;
					key = finalKeys [stage];
					keyHit = true;
					Debug.Log ("Stage "+stage);
					stage++;
				}
			} 
			else if (key == "U") {
				if (Input.GetKeyDown (KeyCode.U)) {
					slots [stage].color = doneC;
					key = finalKeys [stage];
					keyHit = true;
					Debug.Log ("Stage "+stage);
					stage++;
				}
			} 
	
		if(stage >4){
			quizzing = false;
			finishedQuiz = true;
			CleanUpTime ();
			this.GetComponent<PlayerController> ().boostFuel += boostBoost;
		
		}

		//That was a trip.

		//Input.GetKeyDown (KeyCode.L);
		if(Input.anyKeyDown && !keyHit){
			stage = 0;
			key = finalKeys [stage];
			for(int i =0; i <5; i++){
				slots [i].color = new Color (1,1,1,1);
			}
		}
	}


	public void CleanUpTime(){
		//YEAH BOIII... It feels good to clean those dirty pipes. Am I delirious?
		finalKeys.Clear ();
		textures.Clear ();
		stage = 0;
		displayKeys (false);
		for(int i =0; i <5; i++){
			slots [i].color = new Color (1,1,1,1);
		}
		this.GetComponent<PlayerController> ().destroyCause ();
		Debug.Log ("CLEAN UP TIME!!!");

	}


}
