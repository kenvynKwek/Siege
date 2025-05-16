using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;

    // immunity variables
    private bool isImmune = false;
    private SpriteRenderer spriteRenderer;
    private float defaultImmunityDuration = 1.5f;
    private float immunityFlashIntervalDuration = 0.15f;

    public GameObject[] hearts; // health UI

    // event for 0 health
    public delegate void zeroHealthAction();
    public static event zeroHealthAction zeroHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    /// Subtracts damage from the player's currentHealth.
    /// </summary>
    /// <param name="damage">The damage the player will take</param>
    public void TakeDamage(int damage)
    {
        if (isImmune) // if immune, do nth
        {
            return;
        }
        else // else not immune & get hit
        {
            // update health & UI

            currentHealth -= damage;
            StartCoroutine(StartImmunity(defaultImmunityDuration));
        }
    }

    /// <summary>
    /// This function grants the player temporary immunity.
    /// </summary>
    /// <param name="immunityDuration">Total duration of immunity time.</param>
    /// <returns>An IEnumerator for coroutine timing.</returns>
    private IEnumerator StartImmunity(float immunityDuration)
    {
        isImmune = true;
        // check duration, if times up, set immune to false
        float elapsedTime = 0f;

        // flash the sprite
        while (elapsedTime < immunityDuration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(immunityFlashIntervalDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(immunityFlashIntervalDuration);

            elapsedTime += immunityFlashIntervalDuration * 2; // update elapsed time
        }

        isImmune = false;
    }
}
