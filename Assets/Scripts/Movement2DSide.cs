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
    public float dodgeX; // X direction and speed of dodge
    public float dodgeY; // y direction and speed of dodge
    Vector2 facing; // what direction dave is facing

   // public GameObject[] projectile;

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
        ProjectileMvt.Dave = this.gameObject;
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
            // rotation/facing
            // will most likely be replaced by joystick + mouse inputs
            if (x != 0 && y != 0) facing = new Vector2(x, y);
            else {
                if (x != 0) {
                    facing = new Vector2(x, 0);
                }
                if (y != 0) {
                    facing = new Vector2(0, y);
                }
            }
            // movement
            if(Input.GetButtonDown("Dodge") & dodgeCooldown == 0 & di.isTalking == false){ //Dodging, can only occur when not on cooldown and not talking
                // TEMP FOR DEMO set to teleport dodge
                Debug.Log("Dodge");
                //dodgeTimer = 240;
                dodgeCooldown = 1000; // reduced from 2400
                dodgeX = x;
                dodgeY = y;
                // teleports dave and lerps camera
                transform.position = new Vector2(transform.position.x + (speed * x), transform.position.y + (speed * y));
                StartCoroutine(smoothCam());
            }
            /*if(dodgeTimer > 0){ // Dodge roll. Set speed that cannot be influenced until dodge is over
                rb.velocity = new Vector2(dodgeX * speed, dodgeY * speed);
                dodgeTimer--;
            }
            else*/ if(Input.GetButton("Sprint")){ //Sprinting
                rb.velocity = new Vector2(x * speed * 2, y * speed * 2);
            }
            else{
                rb.velocity = new Vector2(x * speed, y * speed);
            }
            if (Input.GetMouseButtonDown(0)) {
                // fire projectile
                Instantiate(Resources.Load("Ice Shard"), new Vector2(transform.position.x + facing.x, transform.position.y + facing.y), Quaternion.identity);    
            }
            else if (Input.GetMouseButtonDown(1)) {
                // ice projectile
                Instantiate(Resources.Load("Fireball2"), new Vector2(transform.position.x + facing.x, transform.position.y + facing.y), Quaternion.identity);
            }
        }  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    IEnumerator smoothCam() { // changes the smooth funct on the camera for a second (for teleport dodge)
        CameraControls.isSmoothed = true;
        yield return new WaitForSeconds(1);
        CameraControls.isSmoothed = false;
    }
}
