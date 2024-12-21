using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
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
}
