using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveStats : MonoBehaviour
{
    [Range (0, 100)]
    public int daveHealth;
    public int daveMaxHealth = 100;
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
        /*if(col.tag == "Enemy" & onHitInvincibleFrames == 0 & gameObject.GetComponent<Movement2DSide>().dodgeTimer == 0){
            //Debug.Log("collision detected");
            GameObject enemy = col.gameObject;
            //int damage = enemy.GetComponent<>();
            daveHealth = daveHealth - damage;
            onHitInvincibleFrames = 240;
        }*/
        if(col.tag == "HealthPickup"){ // Health pickup triggers and is deleted
            if(daveHealth == daveMaxHealth){
                // Does nothing, but doesn't destroy the heart pickup
            }
            else if(daveHealth + 10 > daveMaxHealth){
                daveHealth = daveMaxHealth;
                Destroy(col.gameObject);
            }
            else{
                daveHealth = daveHealth + 10;
                Destroy(col.gameObject);
            }
            
        }
    }
    public void DaveHit(int dmg){
        daveHealth = daveHealth - dmg;
    }
}
