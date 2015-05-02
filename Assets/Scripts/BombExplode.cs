using UnityEngine;
using System.Collections;

public class BombExplode : MonoBehaviour {
    public int ExplodeTime = 5;
    public float explodeTimer = 0f;

    public float scaleSpeed = 0.5f;
    
    public void Update(){
        if(explodeTimer >= ExplodeTime){
            Destroy(gameObject);
        }else{
            explodeTimer += Time.deltaTime;
            transform.localScale += new Vector3(scaleSpeed * Time.deltaTime, 0f,0f);
        }
    }
}