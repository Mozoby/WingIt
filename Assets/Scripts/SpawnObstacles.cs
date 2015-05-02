using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour {
	public PlayerFlap fapper;
	public OVRPlayerController player;
	public ScoreBox obstacle;
	float x = 0;

	public void Update(){

		float y = Random.Range(25f, 60f);

		if (x < 100){
			ScoreBox ob = Instantiate(obstacle, new Vector3(x*20.0f,y,10.0f), Quaternion.identity) as ScoreBox;
			ob.fapper = fapper;
			ob.player = player;
			x++;
		}
	}

}