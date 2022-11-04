using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DaveStats : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private int _daveHealth;
    [SerializeField]
    [Range(0, 100)]
    private int _daveMana;
    [SerializeField]
    [Range(0, 100)]
    private int _daveEnergy;
    
    // Use these fields in the actual code
    // These fields will automatically trigger the appropriate event when they're changed
    public int daveHealth
    {
        get => _daveHealth;
        set
        {
            _daveHealth = value;
            onHealthChange?.Invoke((float)_daveHealth/daveMaxHealth);
        }
    }

    public int daveMana
    {
        get => _daveMana;
        set
        {
            _daveMana = value;
            onManaChange?.Invoke((float)_daveMana/daveMaxMana);
        }
    }

    public int daveEnergy
    {
        get => _daveEnergy;
        set
        {
            _daveEnergy = value;
            onEnergyChange?.Invoke((float)_daveEnergy/daveMaxEnergy);
        }
    }
    
    public int daveMaxHealth = 100;
    public int daveMaxMana = 100;
    public int daveMaxEnergy = 100;

    [HideInInspector]
    public UnityEvent<float> onHealthChange;
    [HideInInspector]
    public UnityEvent<float> onManaChange;
    [HideInInspector]
    public UnityEvent<float> onEnergyChange;
    
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
    public void DaveHeal(){
        daveHealth = daveMaxHealth;
        daveEnergy = daveMaxEnergy;
        daveMana = daveMaxMana;
    }
}
