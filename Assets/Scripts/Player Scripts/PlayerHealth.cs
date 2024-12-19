using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Subtracts damage from the 'Player' game object's currentHealth
    /// </summary>
    /// <param name="damage">The damage the player will take</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Time.timeScale = 0f; // freeze screen
        gameOverScreen.SetActive(true); // display "game over" message
    }
}
