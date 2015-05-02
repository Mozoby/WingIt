using UnityEngine;
using System.Collections;

public class PlayerFlap : MonoBehaviour{

	public OVRPlayerController player;
	public TimedBillboard getReadyBill;
	public TimedBillboard gameOverBill;
	public AudioClip dieSound;
	public AudioClip flap;
	public AudioClip scoreSound;

	public bool hasShownGetReady = false;
	public bool gameOver = false;
	public bool hasShownGameOver = false;
	public int currentScore = 0;

	public void Update(){

		if(Input.GetKeyDown(KeyCode.PageDown) || Input.GetKeyDown(KeyCode.PageUp) || Input.GetKeyDown(KeyCode.F)){
			if(!gameOver){
				player.Jump();
				player.audio.PlayOneShot(flap);
			}
			else{
				revive();
			}
		}

		if(!hasShownGetReady){
			getReadyBill.show();
			hasShownGetReady = true;
		}

		if(gameOver && !hasShownGameOver){
			gameOverBill.show();
			hasShownGameOver = true;
		}
	}

	public void die(){
		gameOver = true;
		player.gameOver = true;
		player.audio.PlayOneShot(dieSound);
	}

	public void revive(){
		gameOver = false;
		player.gameOver = false;
		hasShownGameOver = false;
		player.transform.position = new Vector3(-50f, 90f, 10f);
		currentScore = 0;
	}

	public void score(){
		currentScore += 1;
		player.audio.PlayOneShot(scoreSound);
		Debug.Log(currentScore);
	}
}