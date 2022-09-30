using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    Rigidbody2D rb; // physics and collision
    Animator anim; // animation
    SpriteRenderer sr; // base sprite
    public float framesToMove = 0;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
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
        else if(framesToMove >= 1200)
        {
            x = -1;
        }
        
        float y = 0;

        rb.velocity = new Vector2(x * 1, y * 1);
    }
}
