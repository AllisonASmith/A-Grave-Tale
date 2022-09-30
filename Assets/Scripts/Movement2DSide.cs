using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement2DSide : MonoBehaviour
{
    // player components
    Rigidbody2D rb; // physics and collision
    Animator anim; // animation
    SpriteRenderer sr; // base sprite

    public float Scale; //scale of the game (affects speed, later attack speed)
    [Range (0,1)]
    public float speed; //speed magnitude
    [Range (0,5)]
    public float atkSpeed; // attack cooldown
    [Range (0, 5)]
    public float Friction; //stops object on the floor, use 0 for no friction


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed *= Scale;
        speed *= atkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // keys/joysticks for inputs can be managed in Edit > Project Settings > Input Manager
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        anim.SetInteger("x", Mathf.CeilToInt(x));
        anim.SetInteger("y", Mathf.CeilToInt(y));
        // movement
        if(Input.GetKey("left shift")){
            rb.velocity = new Vector2(x * speed * 2, y * speed * 2);
        }
        else{
            rb.velocity = new Vector2(x * speed, y * speed);
        }
        

        
        
    }
}
