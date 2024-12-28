using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemies automatically move toward the 'player' game object

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private float rotationOffset = -90f;

    public int damage;
    public float moveSpeed;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("hasSpawned", false);

        // set self as invalid object until spawn animation is over
        gameObject.tag = "Untagged";

        // wait for spawn animation
        StartCoroutine(WaitForSpawnAnimation());

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Enemy") // is valid enemy object
        {
            // Calculate the direction from enemy to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Rotate to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset; // Convert to degrees
            transform.rotation = Quaternion.Euler(0f, 0f, angle); // Apply rotation for 2D (z-axis rotation)

            // Move the enemy toward the player independently of rotation
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // deal damage
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null) playerHealth.TakeDamage(damage);

            // destroy self ('enemy' game object)
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitForSpawnAnimation()
    {
        // wait for spawn animation to complete
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        animator.SetBool("hasSpawned", true);
        gameObject.tag = "Enemy";
    }
}
