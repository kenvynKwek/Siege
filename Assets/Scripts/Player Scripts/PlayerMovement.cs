using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 movement;

    private float speed = 1.5f;
    private float accelerationRate = 10f;
    private float decelerationRate = 10f;

    private Vector2 targetVelocity; // The desired velocity


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow)) movement += Vector2.up * speed;
        if (Input.GetKey(KeyCode.DownArrow)) movement += Vector2.down * speed;
        if (Input.GetKey(KeyCode.LeftArrow)) movement += Vector2.left * speed;
        if (Input.GetKey(KeyCode.RightArrow)) movement += Vector2.right * speed;

        // normalize to keep diagonal movement speed consistent w/ up/down/left/right
        if (movement != Vector2.zero) movement = movement.normalized * speed;
        targetVelocity = movement * speed;
    }

    void FixedUpdate()
    {
        // Smoothly interpolate the current velocity towards the target velocity
        if (targetVelocity != Vector2.zero)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, accelerationRate * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate smoothly when no input is detected
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, decelerationRate * Time.fixedDeltaTime);
        }
    }
}
