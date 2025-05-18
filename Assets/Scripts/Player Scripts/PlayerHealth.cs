using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;
    private GameManager gameManager;

    // camera shake variables
    private float shakeDuration = 0.3f;
    private float shakeIntensity = 0.025f;

    // immunity variables
    private SpriteRenderer spriteRenderer;
    private float defaultImmunityDuration = 1.5f;
    private float immunityFlashIntervalDuration = 0.15f;
    public bool isImmune = false;

    public GameObject[] hearts; // health UI
    public GameObject hitEffectPrefab; // hit particle effect

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Updates the heatlh UI based on the "currentHealth" variable.
    /// </summary>
    public void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth) hearts[i].SetActive(true);
            else hearts[i].SetActive(false);
        }
    }

    /// <summary>
    /// Subtracts damage from the player's currentHealth.
    /// </summary>
    /// <param name="damage">The damage the player will take.</param>
    /// <param name="hitDirection">The direction the damage came from.</param>
    public void TakeDamage(int damage, Vector2 hitDirection)
    {
        if (isImmune) // if immune, do nth
        {
            return;
        }
        else // else not immune & get hit
        {
            // update health & UI
            ////////////currentHealth -= damage;
            UpdateHealthUI();

            // death check
            if (currentHealth <= 0)
            {
                gameManager.GameOver();
            }
            else // not dead
            {
                CameraShake.Instance.ShakeCamera(shakeDuration, shakeIntensity); // shake screen

                // spawn hit particle effect at hit direction
                float hitDirectionAngle = (float) (Math.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg);
                hitDirectionAngle *= -1f; // negate to mirror to get correct angle
                Quaternion hitRotation = Quaternion.Euler(hitDirectionAngle, 90f, 0f);
                GameObject hitParticleEffect = Instantiate(hitEffectPrefab, transform.position, hitRotation);
                Destroy(hitParticleEffect, 0.25f); // destroy after

                StartCoroutine(StartImmunity(defaultImmunityDuration)); // temp player immunity
            }
        }
    }

    /// <summary>
    /// Grants the player temporary immunity.
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
