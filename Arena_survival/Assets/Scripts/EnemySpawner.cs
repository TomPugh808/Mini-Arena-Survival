
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Pool")]
    public GameObject enemyPrefab;
    public int poolSize = 20;

    private GameObject[] enemies;

    [Header("Wave Settings")]
    public int startingEnemies = 3;
    public float timeBetweenSpawns = 1f;

    private int currentWave = 1;
    private int enemiesRemainingToSpawn;
    private int enemiesAlive;

    [Header("Spawn Area")]
    public float spawnRangeX = 6.5f;
    public float spawnRangeY = 3.5f;

    void Awake()
    {
        enemies = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity); // create enemys beforehand
            enemy.SetActive(false);
            enemies[i] = enemy;

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.SetSpawner(this);
            }
        }
    }

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        enemiesRemainingToSpawn = startingEnemies + (currentWave - 1) * 2;

        Debug.Log("Wave " + currentWave + " started!");

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesRemainingToSpawn > 0)
        {
            GameObject enemy = GetFreeEnemy();

            if (enemy == null)
            {
                Debug.LogWarning("No free enemies in pool!");
                yield break;
            }

            
        Vector2 spawnPosition;

        int side = Random.Range(0, 4);

        switch (side) // this is to spawn enemies at edge of room
        {
            case 0: // Top
                spawnPosition = new Vector2(
                    Random.Range(-spawnRangeX, spawnRangeX),
                    spawnRangeY
                );
                break;

            case 1: // Bottom
                spawnPosition = new Vector2(
                    Random.Range(-spawnRangeX, spawnRangeX),
                    -spawnRangeY
                );
                break;

            case 2: // Left
                spawnPosition = new Vector2(
                    -spawnRangeX,
                    Random.Range(-spawnRangeY, spawnRangeY)
                );
                break;

            default: // Right
                spawnPosition = new Vector2(
                    spawnRangeX,
                    Random.Range(-spawnRangeY, spawnRangeY)
                );
                break;
        }


            enemy.transform.position = spawnPosition;

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.ResetHealth();
            }

            enemy.SetActive(true);

            enemiesAlive++;
            enemiesRemainingToSpawn--;

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    GameObject GetFreeEnemy()
    {
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeSelf)
            {
                return enemy;
            }
        }

        return null;
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && enemiesRemainingToSpawn <= 0)
        {
            currentWave++;
            StartWave();
        }
    }
}
