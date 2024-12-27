using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// automatically shoots projectiles from attached game object center to targets in a radius

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate;
    public float shootingRange;

    private float nextFireTime = 0f;
    private float rotationOffset = -90f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                Shoot(target);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    /// <summary>
    /// Shoots a projectile towards a given target.
    /// </summary>
    /// <param name="target">The game object to shoot at.</param>
    void Shoot(GameObject target)
    {
        // get direction to target
        Vector2 direction = (target.transform.position - transform.position).normalized;

        // rotate to face enemy (note to self: may have to offset rotation because sprite)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;

        // instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));

        // access projectile attributes
        ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
        float projectileSpeed = projectileScript.projectileSpeed;

        // set velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        // ignore collision w/ pl○ayer
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, projectileCollider);
    }

    /// <summary>
    /// Finds the nearest enemy to the player game object.
    /// </summary>
    /// <returns>The nearest enemy as a game object.</returns>
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= shootingRange)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
