using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    /*
     * This is the data class for basic enemy stats/functs
     * References to enemies can be sent through EnemyManager - Al
     */

    // enemy identifiers
    [SerializeField]
    private string _type; // identifier for prefab + texture loading, use file name from Resources/Enemies
    //private GameObject obj; // the gameobject in scene being referenced
    // Enemy stats
    [SerializeField]
    private int _currentHP; // amount of health the enemy currently has, maxHP - currentHP
    [SerializeField]
    [Range(0, 500)]
    private int _maxHP; // max health
    [SerializeField]
    [Range(0, 50)]
    private int _attack; // attack power
    // defense
    // other base stats
    // stat boosts that will probably be another struct, maybe make the values some structure too
    // misc characteristics
    private bool _isRespawnable; // true if the enemy can be respawned, false otherwise
    // for AI
    public float framesToMove = 0;
    public int framesStunned;
    public GameObject Dave; // because everyone needs Dave
    bool swooping; // AI swooping, temp value for now

    public void init(string type, int HP, int attack, bool isRespawnable, GameObject target) {
        _type = type;
        _currentHP = HP;
        _maxHP = HP;
        _attack = attack;
        _isRespawnable = isRespawnable;
        Dave = target;
    }

    /*
     * getters and setters
     */
    public string type { get => _type; }
    public GameObject obj { get => this.gameObject; }
    public int currentHP {
        get => _currentHP;
        set {
            _currentHP = value;
        }
    }
    public int maxHP {
        get => _maxHP;
        set {
            _maxHP = value;
        }
    }
    public int attack {
        get => _attack;
        set {
            _attack = value;
        }
    }
    public bool isRespawnable { 
        get => _isRespawnable;
        set {
            _isRespawnable = value;
        }
    }

    /*
     * Base enemy functions and overrides
     * Behavior traits should be placed here,and anything specific per enemy can be put as an override or in another script
     * This is a straight copy from Enemy_Movement and needs to be updated
     */
    public void Update()
    {
        if (swooping == false)
        {
            if (framesStunned > 0)
            {
                framesStunned--;
            }
            else if ((Vector2.Distance(transform.position, Dave.transform.position) < 5) & (Vector2.Distance(transform.position, Dave.transform.position) > 2))
            {
                transform.position = Vector2.MoveTowards(transform.position, Dave.transform.position, (float).01);
            }
            else if (Vector2.Distance(transform.position, Dave.transform.position) <= 2)
            { //Once the bat is close enough, swoop towards Dave
                swooping = true;
                StartCoroutine(swoop());
            }
            else
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
                else if (framesToMove >= 1200)
                {
                    x = -1;
                }

                float y = 0;

                GetComponent<Rigidbody2D>().velocity = new Vector2(x * 1, y * 1);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    { // Damage Dave
        if (col.name == "Dave")
        {
/*            if (Input.GetButton("Fire1"))
            { // Test interaction with dave attacking
                _currentHP = HP - ds.daveDamage;
                framesStunned = 120;
                if (HP <= 0)
                { // Dies
                    Instantiate(DroppedHealthPickup, bat.transform.position, bat.transform.rotation); // Drops a health pickup by cloning a health pickup that is outside of the map
                    Destroy(this);
                }
            }*/
            //else
            //{
                col.GetComponent<DaveStats>().DaveHit(_attack);
            //}
        }
    }
    IEnumerator swoop()
    {
        Vector2 targetPosition = transform.position + (Dave.transform.position - transform.position).normalized * 1000.0f;
        //Debug.Log(targetPosition);
        //var swoopDirection = targetPosition - transform.position;
        //transform.LookAt(targetPosition);
        for (int i = -30; i <= 30; i++)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, (float)(0.003 * Math.Abs((Math.Abs(i) - 30))));
            //Debug.Log(transform.position);
            //Debug.Log(targetPosition);
            yield return new WaitForSeconds((float)0.01);
        }
        swooping = false;

    }
}

