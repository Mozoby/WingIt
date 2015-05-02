using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	public OVRPlayerController player;
	public AudioClip explodeSound;
	public ExplosionMat bombExplode;
	public Texture2D bombRamp;

	public readonly int ExplodeTime = 1;
	public bool hasExploded = false;
	public float explodeTimer = 0f;

	public readonly float Speed = -50;

    public void Update(){
    	if(!hasExploded){
	    	transform.Translate(new Vector3(Speed * Time.deltaTime,0,0));
	    	if(transform.position.x <= player.transform.position.x){
	    		Debug.Log("Exploding");
	    		Explode();
	    	}
	    } else{
	    	explodeTimer += Time.deltaTime;
	    	if(explodeTimer >= ExplodeTime)
	    		Destroy(gameObject);
	    }
    }

    public void Explode(){
    	if(!hasExploded){
	    	hasExploded = true;
	    	/// woooo i wish
	    }
    }


}