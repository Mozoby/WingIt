using UnityEngine;
using System.Collections;

public class TimedBillboard : MonoBehaviour{

	public GameObject element;
	public OVRPlayerController controller;

	public readonly int TimeSeen = 2;
	public float shownTime = 0;
	public bool isShowing = false;


	public void Update(){
		if(isShowing){
			shownTime += Time.deltaTime;
			if(shownTime > TimeSeen){
				hide();
			}
		}
	}

	public void show(){
		isShowing = true;
		shownTime = 0;
		element.SetActive(true);	
	}

	public void hide(){
		isShowing = false;
		element.SetActive(false);
	}
}