using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;
    private bool isImmune = false;
    private SpriteRenderer spriteRenderer;

    public GameObject[] hearts; // health UI

    // event for 0 health
    public delegate void zeroHealthAction();
    public static event zeroHealthAction zeroHealth;

    // immunity variables
    public float immunityDuration;
    public float flashInterval;

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
    /// Subtracts damage from the 'Player' game object's currentHealth
    /// </summary>
    /// <param name="damage">The damage the player will take</param>
    public void TakeDamage(int damage)
    {
        // if immune, do nth
        if (isImmune)
        {
            return;
        }
        else
        {
            // else not immune & get hit
            // set immune to true
            // update health & UI
            // currentHealth -= damage;
            StartCoroutine(startImmunity());
        }
    }

    private IEnumerator startImmunity()
    {
        isImmune = true;
        // check duration, if times up, set immune to false
        float elapsedTime = 0f;

        // flash the sprite
        while (elapsedTime < immunityDuration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashInterval);

            elapsedTime += flashInterval * 2; // update elapsed time
        }


        isImmune = false;
    }
}
