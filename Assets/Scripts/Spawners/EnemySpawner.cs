using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomly spawn an enemy at regular intervals outside the play area perimeter

public class EnemySpawner : MonoBehaviour
{
    private float spawnTimer = 0f;
    private bool canSpawn = false;

    public GameObject enemy;
    public float spawnRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            spawnTimer += Time.unscaledDeltaTime;

            if (spawnTimer >= spawnRate)
            {
                Vector3 spawnPoint = getRandomEnemySpawnPoint();
                Instantiate(enemy, spawnPoint, transform.rotation);

                spawnTimer = 0f;
            }
        }
    }

    /// <summary>
    /// Gets a random spawn point for the 'enemy' game object within the play area perimeter
    /// </summary>
    /// <returns>A random position as a Vector3</returns>
    Vector3 getRandomEnemySpawnPoint()
    {
        // spawn perimeters just outside of play area
        float left = -2.45f;
        float right = 2.45f;
        float top = 1.4f;
        float bottom = -1.4f;

        Vector3 spawnPoint = new Vector3(Random.Range(left, right), Random.Range(bottom, top), 0f);

        return spawnPoint;
    }

    /// <summary>
    /// Enables or disables enemy spawning.
    /// </summary>
    /// <param name="spawn">True or false to enable enemy spawning.</param>
    public void SetCanSpawn(bool spawn)
    {
        canSpawn = spawn;
    }
}
