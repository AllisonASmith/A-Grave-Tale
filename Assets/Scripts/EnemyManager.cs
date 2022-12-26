using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /*
    * This is the basic manager for all enemies in a scene
    * Any value that applies to every enemy should go here - Al
    */

    // list of enemies in the scene
    public IDictionary<string, Enemy> enemyManager = new Dictionary<string, Enemy>();
    public GameObject Dave;
    
    // Start is called before the first frame update
    void Start()
    {
        addEnemy(20, 5, true, new Vector2(1, 1), "Batto", "Bat");
    }

    /*
     * get/set enemy traits from dictionary
     */
    public int getCurrentHP(string enName)
    {
        // get enemy enName's HP 
        if (enemyManager.ContainsKey(enName))
        {
            return enemyManager[enName].currentHP;
        }
        else
        {
            Debug.Log("Attempted to reference a non-existent enemy");
            return -1;
        }
    }
    public void setCurrentHP(string enName, int amount, bool isAdd = false)
    {
        // add to or set amount of enemy currentHP
        if (enemyManager.ContainsKey(enName))
        {
            Enemy temp = enemyManager[enName];
            if (isAdd) temp.currentHP = temp.currentHP + amount;
            Debug.Log("hp after everything: " + temp.currentHP);
            if (temp.currentHP <= 0) {
                Destroy(temp.gameObject);
                enemyManager.Remove(enName);
            }
        }
        else {
            Debug.Log("Attempted to reference a non-existent enemy");
            return;
        }
    }
    public int getMaxHP(string enName)
    {
        // get maxHP by object name
        if (enemyManager.ContainsKey(enName))
        {
            return enemyManager[enName].maxHP;
        }
        else
        {
            Debug.Log("Attempted to reference a non-existent enemy");
            return -1;
        }
    }
    public int getAttack(string enName)
    {
        // return enemy enName's attack value
        if (enemyManager.ContainsKey(enName))
        {
            return enemyManager[enName].attack;
        }
        else
        {
            Debug.Log("Attempted to reference a non-existent enemy");
            return -1;
        }
    }
    public void setAttack(string enName, int attack)
    {
        // change the base value of enemy enName's attack
        if (enemyManager.ContainsKey(enName))
        {
            enemyManager[enName].attack = attack;
        }
        else
        {
            Debug.Log("Attempted to reference a non-existent enemy");
        }
    }

    /*
     * add to dictionary
     */
    public void addEnemy(int HP, int attack, bool isRespawnable, Vector2 position, string enName, string type)
    {
        // load an enemy prefab from "Assets/Resources/Enemies/" + type at spawn location position
        GameObject newObj = (GameObject)Instantiate(Resources.Load("Enemies/" + type));
        newObj.transform.SetParent(this.transform);
        newObj.name = enName;
        newObj.transform.position = position;
        Enemy temp =  newObj.GetComponent<Enemy>();
        temp.init(type, HP, attack, isRespawnable, Dave);
        enemyManager.Add(enName, temp);
        // temp, need to sort dave and other prefabs later
        newObj.GetComponent<Enemy>().Dave = GameObject.Find("Dave");
    }
    public void addEnemy(int HP, int attack, bool isRespawnable, GameObject enemy)
    {
        // adds an existing gameobject from the scene to the list of enemies
        Enemy temp = enemy.AddComponent<Enemy>();
        temp.init(enemy.name, HP, attack, isRespawnable, Dave);
        enemy.transform.SetParent(this.transform);
        enemyManager.Add(enemy.name, temp);
    }
}
