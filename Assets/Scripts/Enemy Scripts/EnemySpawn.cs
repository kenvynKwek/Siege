using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomly spawn an enemy at regular intervals outside the play area perimeter

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    private enum SpawnArea
    {
        Left,
        Right,
        Top,
        Down
    }
    private float spawnTimer = 0f;
    private float spawnRate = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            Vector3 spawnPoint = getRandomEnemySpawnPoint();
            Instantiate(enemy, spawnPoint, transform.rotation);

            spawnTimer = 0f;
        }
    }

    /// <summary>
    /// Gets a random spawn point for the 'enemy' game object that is outside the play area perimeter
    /// </summary>
    /// <returns>A random position as a Vector3</returns>
    Vector3 getRandomEnemySpawnPoint()
    {
        // spawn perimeters just outside of play area
        float leftSpawnLine = -1.70f;
        float rightSpawnLine = 1.70f;
        float topSpawnLine = 1.0f;
        float bottomSpawnLine = -1.0f;

        int spawnArea = Random.Range(0, 4); // get a random spawn area (left/right/top/down)
        Vector3 spawnPoint = Vector3.zero;

        // get a spawn point within a spawn area
        switch ((SpawnArea)spawnArea)
        {
            case SpawnArea.Left:
                spawnPoint = new Vector3(leftSpawnLine, Random.Range(bottomSpawnLine, topSpawnLine), 0f);
                break;
            case SpawnArea.Right:
                spawnPoint = new Vector3(rightSpawnLine, Random.Range(bottomSpawnLine, topSpawnLine), 0f);
                break;
            case SpawnArea.Top:
                spawnPoint = new Vector3(Random.Range(leftSpawnLine, rightSpawnLine), topSpawnLine, 0f);
                break;
            case SpawnArea.Down:
                spawnPoint = new Vector3(Random.Range(leftSpawnLine, rightSpawnLine), bottomSpawnLine, 0f);
                break;
            default:
                break;
        }

        return spawnPoint;
    }
}
