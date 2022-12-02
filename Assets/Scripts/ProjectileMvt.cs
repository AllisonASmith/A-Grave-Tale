using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMvt : MonoBehaviour
{
    float timer = 10;
    public static GameObject Dave;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Vector2 dir = Dave.transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = dir * -5;
        if (timer < 0) Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            //do damage to enemy
            collision.gameObject.GetComponent<EnemyStats>().setDamage(collision.gameObject.name, 5, true);
        }
        Destroy(this.gameObject);
    }
}
