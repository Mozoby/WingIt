using UnityEngine;
using System.Collections;

public class PlayerDie : MonoBehaviour {
    
	public PlayerFlap fapper;

    void OnCollisionEnter(Collision collider) {
        fapper.die();
        Debug.Log("col");
    }

    void OnControllerColliderHit(ControllerColliderHit hit){
    	if(hit.collider.tag == "NoDie"){
    		fapper.score();
    	}
    	if(!fapper.gameOver)
    		fapper.die();
    }
}