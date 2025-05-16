using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public Animator animator;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    /// <summary>
    /// Takes a given amount of damage.
    /// </summary>
    /// <param name="damage">The damage to receive.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    /// <summary>
    /// Destroy the enemy game object.
    /// </summary>
    private void DestroyEnemy()
    {
        // make enemy invalid to disable movement & not be targeted by projectiles
        gameObject.tag = "Untagged";

        // destroy animation
        animator.SetBool("isDestroyed", true);

        float destroyAnimationDelay = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, destroyAnimationDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // deal damage
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.isImmune)
            {
                playerHealth.TakeDamage(damage);

                // destroy self
                Destroy(gameObject);
            }
        }
    }
}
