using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bounces the player back in the opposite direction upon collision

public class WallScript : MonoBehaviour
{
    public float bounceForce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // get player rigidbody
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // get opposite of player direction
            Vector2 playerVelocity = rb.velocity;
            Vector2 oppositeDirection = -playerVelocity.normalized;

            // bounce player back
            rb.velocity = oppositeDirection * bounceForce;
        }
    }
}
