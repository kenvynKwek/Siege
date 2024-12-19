using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;

    // event for 0 health
    public delegate void zeroHealthAction();
    public static event zeroHealthAction zeroHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) zeroHealth?.Invoke();
    }

    /// <summary>
    /// Subtracts damage from the 'Player' game object's currentHealth
    /// </summary>
    /// <param name="damage">The damage the player will take</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
