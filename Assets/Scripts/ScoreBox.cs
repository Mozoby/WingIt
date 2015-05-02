using UnityEngine;
using System.Collections;

public class ScoreBox : MonoBehaviour {
    
	public PlayerFlap fapper;
	public OVRPlayerController player;
	public bool hasScored = false;

	public void Update(){
		if(player.transform.position.x > transform.position.x && !hasScored){
			fapper.score();
			hasScored = true;
		}
	}
}