using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler  {

	//Virtual Joystick
	private Image backgroundImg;
	private Image joystickImg;
	private Vector3 inputVector;

	// Use this for initialization
	void Start () {
		backgroundImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	//virtual joystick
	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle (backgroundImg.rectTransform,ped.position,ped.pressEventCamera, out pos)){
			pos.x = (pos.x / backgroundImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / backgroundImg.rectTransform.sizeDelta.y);
			inputVector = new Vector3 (pos.x * 2 - 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
			joystickImg.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (backgroundImg.rectTransform.sizeDelta.x /3),
				inputVector.z * (backgroundImg.rectTransform.sizeDelta.y / 3));
		}
	}
	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}
	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}
	public float Horizontal(){
		if (inputVector.x != 0) {
			if(inputVector.x<0){
				return inputVector.x - 0.5f;
			}
			else if (inputVector.x>0){
				return inputVector.x + 0.5f;
			}
			return inputVector.x;
		} else{
			return Input.GetAxis ("Horizontal");
		}
	}

	public float Vertical(){
		if(inputVector.z != 0){
			if(inputVector.z>0){
				return inputVector.z + 0.5f;
			}
			return inputVector.z;
		}else{
			return Input.GetAxis ("Vertical");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
