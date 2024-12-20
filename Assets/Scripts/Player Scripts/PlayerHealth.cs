using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;

    // health UI
    public GameObject[] hearts;

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

        // health UI
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth) hearts[i].SetActive(true);
            else hearts[i].SetActive(false);
        }
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
