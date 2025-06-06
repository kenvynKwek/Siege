using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private GameObject gameManager;

    public float health;
    public Animator animator;
    public int damage;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Takes a given amount of damage.
    /// </summary>
    /// <param name="damage">The damage to receive.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(DestroyEnemy());
        }
    }

    /// <summary>
    /// Stops movement, transition to destroy animation and destroy self.
    /// </summary>
    /// <returns>An IEnumerator for coroutine timing.</returns>
    private IEnumerator DestroyEnemy()
    {
        // make enemy invalid to disable movement & not be targeted by projectiles
        gameObject.tag = "Untagged";

        gameManager.GetComponent<GameManager>().AddKillCount(); // increment kill count

        // transition to destroy animation
        animator.SetBool("isDestroyed", true);

        yield return null; // wait 1 frame for animator to update state

        float destroyAnimationDelay = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(destroyAnimationDelay); // wait until animation is over
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Enemy") // if enemy is "valid"
        {
            if (collision.CompareTag("Player")) // if collide with player
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
}
