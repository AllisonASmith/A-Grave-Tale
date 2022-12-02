using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_Movement : MonoBehaviour
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
    EnemyStats bt; // Reference to EnemyStats script
    //public Enemy bat;
    public GameObject DroppedHealthPickup;
    public GameObject bat;
    public float framesToMove = 0;
    //Enemy stats = new Enemy();
    public int dmg;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Enemy_Movement script");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //ds = GetComponent<DaveStats>();
        //bt = GetComponent<EnemyStats>();
        bt = FindObjectOfType<EnemyStats>();
        ds = FindObjectOfType<DaveStats>();
        //stats = gameObject.GetComponent<EnemyStats>().getEnemy("bat");
        dmg = bt.getDamage("bat2"); // Gets the damage value of the enemy
        HP = bt.getMaxHP("bat2"); // Gets max hp
        //Debug.Log(dmg);
    }
     
    
    
    //dmg = bt.bat.damage;

    // Update is called once per frame
    void Update()
    {
        // Enemy moves right for 1200 frames, then left for 1200 frames. Patrol mode
        float y = 0;
        framesToMove++;
        if (framesToMove > 2400)
        {
            framesToMove = 0;
            y = 1;
        }
        else if (framesToMove < 1200)
        {
            y = 1;
        }
        else if(framesToMove >= 1200)
        {
            y = -1;
        }
        
        float x = 0;

        rb.velocity = new Vector2(x * 1, y * 1);
        
    }
    void OnTriggerEnter2D(Collider2D col){ // Damage Dave
        if(col.name == "Dave"){
            if(Input.GetButton("Fire1")){ // Test interaction with dave attacking
                HP = HP - ds.daveDamage;
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
}
