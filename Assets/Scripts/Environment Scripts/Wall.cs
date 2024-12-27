using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bounces the player back in the opposite direction upon collision

public class Wall : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // get player rigidbody
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // get bounce direction perpendicular to wall surface
                // -ve to invert because somehow the wall is upside down
                Vector2 collisionNormal = -collision.GetContact(0).normal;

                // bounce player back
                rb.AddForce(collisionNormal * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
