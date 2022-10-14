using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    EnemyStats bt; // Reference to EnemyStats script
    //public Enemy bat;
    public float framesToMove = 0;
    //Enemy stats = new Enemy();
    public int dmg;
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
        dmg = bt.getDamage("bat"); // Gets the damage value of the enemy
        //Debug.Log(dmg);
    }
     
    
    
    //dmg = bt.bat.damage;

    // Update is called once per frame
    void Update()
    {
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
    void OnTriggerEnter2D(Collider2D col){ // Damage Dave
        if(col.name == "Dave"){
            ds.DaveHit(dmg);
        }
    }
}
