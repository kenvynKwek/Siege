using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keep the player game object within the main camera view/bounds
// The player is "bounced" off the "walls" if it reaches the outer bounds => thus keeping player within the play area

public class PlayerBounds : MonoBehaviour
{
    public Rigidbody2D rb;

    private float xMin = -1.53f;
    private float xMax = 1.53f;
    private float yMin = -0.82f;
    private float yMax = 0.82f;
    private float bounceForce = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // check range instead of ==, rarely able to detect precisely == to a point
        if (rb.position.x <= xMin) rb.velocity = new Vector2(bounceForce, rb.velocity.y); // Apply bounce force to the right
        if (rb.position.x >= xMax) rb.velocity = new Vector2(-bounceForce, rb.velocity.y);
        if (rb.position.y <= yMin) rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        if (rb.position.y >= yMax) rb.velocity = new Vector2(rb.velocity.x, -bounceForce);
    }
}
