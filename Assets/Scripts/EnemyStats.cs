using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Defines enemies
    public struct Enemy{
        public string name;
        public int damage;
        public int max_health;
    }

    public static Enemy[] enemies = new Enemy[]{
        new Enemy() {name = "bat", damage = 10, max_health = 40}
    };
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(enemies[0].name);
        
    }

    public int getDamage(string enName){
        foreach(Enemy i in enemies){
                /*Debug.Log(i.name);
                Debug.Log(i.damage);
                Debug.Log(i.max_health);*/
            if(i.name == enName){
                //Debug.Log(i.name);
                //Debug.Log(i.damage);
                //Debug.Log(i.max_health);
                return i.damage;
            }
        }
        Debug.Log("Attempted to reference a non-existent enemy");
        return 0;
    }
        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

