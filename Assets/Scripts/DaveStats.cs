using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveStats : MonoBehaviour
{
    [Range (0, 10)]
    public int daveHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(daveHealth == 0){
            Debug.Log("dead");
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.name == "Enemy"){
            Debug.Log("collision detected");
            daveHealth--;
        }
    }
    public int getHealth(){
        return daveHealth;
    }
}
