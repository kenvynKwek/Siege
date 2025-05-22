using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float originalSpeed = 1.1f;
    private float speed;
    private float slowedSpeed = 0.85f;

    public Rigidbody2D rb;
    public float accelerationRate;
    public float decelerationRate;

    private Vector2 movement;
    private Vector2 targetVelocity; // The desired velocity


    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
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

    /// <summary>
    /// Slows player's speed temporarily.
    /// </summary>
    /// <param name="duration">The duration for how long speed should be slowed.</param>
    /// <returns></returns>
    private IEnumerator SlowCoroutine(float duration)
    {
        speed = slowedSpeed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    /// <summary>
    /// Applies a slow effect for player movement.
    /// </summary>
    /// <param name="duration">The duration for how long the player should be slowed.</param>
    public void ApplySlow(float duration)
    {
        StopCoroutine("SlowCoroutine");
        StartCoroutine(SlowCoroutine(duration));
    }

    /// <summary>
    /// Stops the player movement.
    /// </summary>
    public void StopMovement()
    {
        speed = 0f;
    }
}
