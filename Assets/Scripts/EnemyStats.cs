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
        new Enemy() {name = "bat1", damage = 10, max_health = 20},
        new Enemy() {name = "bat2", damage = 10, max_health = 20}
    };

    public GameObject Bat1;
    public GameObject Bat2;
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(enemies[0].name);
        //Instantiate(Bat1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        //Instantiate(Bat2, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
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
    public int getMaxHP(string enName){
        foreach(Enemy i in enemies){
                /*Debug.Log(i.name);
                Debug.Log(i.damage);
                Debug.Log(i.max_health);*/
            if(i.name == enName){
                //Debug.Log(i.name);
                //Debug.Log(i.damage);
                //Debug.Log(i.max_health);
                return i.max_health;
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

