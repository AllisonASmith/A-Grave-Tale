using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveStats : MonoBehaviour
{
    [Range (0, 10)]
    public int daveHealth;
    int onHitInvincibleFrames;
    
    // Start is called before the first frame update
    void Start()
    {
        onHitInvincibleFrames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Invincibility Frames: " + onHitInvincibleFrames);
        if(onHitInvincibleFrames > 0){
            //Debug.Log("Invincible");
            onHitInvincibleFrames--;
        }
        //if(daveHealth == 0){
            //Debug.Log("dead");
        //}
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Enemy" & onHitInvincibleFrames == 0){
            //Debug.Log("collision detected");
            daveHealth--;
            onHitInvincibleFrames = 240;
        }
    }
}
