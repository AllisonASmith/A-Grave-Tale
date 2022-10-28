using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement2DSide : MonoBehaviour
{
    // player components
    Rigidbody2D rb; // physics and collision
    Animator anim; // animation
    public SpriteRenderer sr; // base sprite
    DialogueScript di;
    public Sprite baseTalking;

    public float Scale; //scale of the game (affects speed, later attack speed)
    [Range (0,1)]
    public float speed; //speed magnitude
    [Range (0,5)]
    public float atkSpeed; // attack cooldown
    [Range (0, 5)]
    public float Friction; //stops object on the floor, use 0 for no friction
    int dodgeCooldown; // Number of frames left before a new dodge can be used
    public int dodgeTimer; // Number of frames that a dodge lasts


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        di = FindObjectOfType<DialogueScript>();
        speed *= Scale;
        speed *= atkSpeed;
        dodgeCooldown = 0;
        dodgeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(dodgeCooldown > 0){
            dodgeCooldown--;
        }
        //Debug.Log(di.GetComponent<DialogueScript>().isTalking);
        // Stops animation when Dave dies or talks to someone
        if(gameObject.GetComponent<DaveStats>().daveHealth == 0 || di.GetComponent<DialogueScript>().isTalking){
            rb.velocity = new Vector2(0,0);
            anim.gameObject.GetComponent<Animator>().enabled = false;
            sr.sprite = baseTalking;
            //sr.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else{
            anim.gameObject.GetComponent<Animator>().enabled = true;
            sr.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            // keys/joysticks for inputs can be managed in Edit > Project Settings > Input Manager
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            anim.SetInteger("x", Mathf.CeilToInt(x));
            anim.SetInteger("y", Mathf.CeilToInt(y));
            // movement
            if(Input.GetKeyDown("space") & dodgeCooldown == 0){ //Dodging
                //Debug.Log("Dodge begin");
                dodgeTimer = 240;
                dodgeCooldown = 2400;
            }
            if(dodgeTimer > 0){
                rb.velocity = new Vector2(x * speed * 3, y * speed * 3);
                dodgeTimer--;
            }
            else if(Input.GetKey("left shift")){ //Sprinting
                rb.velocity = new Vector2(x * speed * 2, y * speed * 2);
            }
            else{
                rb.velocity = new Vector2(x * speed, y * speed);
            }
        }

        
        
    }
}
