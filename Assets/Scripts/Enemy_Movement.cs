using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Note: I am going to be using this script as the base for enemy movement and will copy and modify it for other enemies. 
// As such, this one will have the most documentation. 
// Please let me know if there is a way to do it where each enemy has movement done in a single script, but that appears to not be possible
// Kyle
public class Enemy_Movement : MonoBehaviour
{
    /*public struct Enemy{
        public string name;
        public int damage;
        public int max_health;
    }*/
    Rigidbody2D rb; // physics and collision
    Animator anim; // animation
    SpriteRenderer sr; // base sprite
    DaveStats ds; // Reference to DaveStats script
    EnemyManager bt; // Reference to EnemyStats script
    //public Enemy bat;
    public GameObject DroppedHealthPickup;
    public GameObject bat;
    public GameObject Dave;
    public float framesToMove = 0;
    //Enemy stats = new Enemy();
    public int dmg;
    public int HP;
    public int framesStunned;
    public bool swooping;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Enemy_Movement script");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //ds = GetComponent<DaveStats>();
        //bt = GetComponent<EnemyStats>();
        bt = GetComponentInParent<EnemyManager>();
        ds = FindObjectOfType<DaveStats>();
        //stats = gameObject.GetComponent<EnemyStats>().getEnemy("bat");
        dmg = bt.getCurrentHP(this.name); // Gets the damage value of the enemy
        HP = bt.getMaxHP(this.name); // Gets max hp
        //Debug.Log(dmg);
    }
     
    
    
    //dmg = bt.bat.damage;

    // Update is called once per frame
    void Update()
    {
        
        if(swooping == false){
            if(framesStunned > 0){
                framesStunned--;
            }
            else if((Vector2.Distance(transform.position, Dave.transform.position) < 5) & (Vector2.Distance(transform.position, Dave.transform.position) > 2)){
                transform.position = Vector2.MoveTowards(transform.position, Dave.transform.position, (float).01);
            }
            else if(Vector2.Distance(transform.position, Dave.transform.position) <= 2){ //Once the bat is close enough, swoop towards Dave
                swooping = true;
                StartCoroutine(swoop());
            }
            else{
                // Enemy moves right for 1200 frames, then left for 1200 frames. Patrol mode
                float x = 0;
                framesToMove++;
                if (framesToMove > 2400)
                {
                    framesToMove = 0;
                    x = 1;
                }
                else if (framesToMove < 1200)
                {
                    x = 1;
                }
                else if(framesToMove >= 1200)
                {
                    x = -1;
                }
        
                float y = 0;

                rb.velocity = new Vector2(x * 1, y * 1);
            }
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D col){ // Damage Dave
        if(col.name == "Dave"){
            if(Input.GetButton("Fire1")){ // Test interaction with dave attacking
                HP = HP - ds.daveDamage;
                framesStunned = 120;
                if(HP <= 0){ // Dies
                    Instantiate(DroppedHealthPickup, bat.transform.position, bat.transform.rotation); // Drops a health pickup by cloning a health pickup that is outside of the map
                    Destroy(bat);
                }
            }
            else{
                ds.DaveHit(dmg);
            }
        }
    }
    IEnumerator swoop(){
        Vector2 targetPosition = transform.position + (Dave.transform.position - transform.position).normalized * 1000.0f;
        //Debug.Log(targetPosition);
        //var swoopDirection = targetPosition - transform.position;
        //transform.LookAt(targetPosition);
        for(int i = -30; i <= 30; i++){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, (float)(0.003*Math.Abs((Math.Abs(i)-30))));
            //Debug.Log(transform.position);
            //Debug.Log(targetPosition);
            yield return new WaitForSeconds((float)0.01);
        }
        swooping = false;
        
    }
    // Copy of existing code that causes the enemy to attack, then teleport behind Dave to attack again. Pretty cool, but doesn't fit a bat. Could be used for a future, more advanced enemy
    /*IEnumerator swoop(){
        Vector2 targetPosition = transform.position + (Dave.transform.position - transform.position).normalized * 1000.0f;
        Debug.Log(targetPosition);
        //var swoopDirection = targetPosition - transform.position;
        //transform.LookAt(targetPosition);
        for(int i = -30; i <= 30; i++){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, (float)(0.005*Math.Abs((Math.Abs(i)-30))));
            Debug.Log(transform.position);
            Debug.Log(targetPosition);
        }
        swooping = false;
        yield return new WaitForSeconds(1);
    }*/ 
}
