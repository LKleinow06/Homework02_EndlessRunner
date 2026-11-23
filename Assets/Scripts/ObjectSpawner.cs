using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Obstacle prefabs")]
    public GameObject[] obstaclePrefabs;

    [Header("Coin prefab")]
    public GameObject coinPrefab;

    [Header("Spawn settings")]
    public float spawnPositionX = 12f;
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 3.5f;

    [Header("Obstacle height")]
    public float obstacleMinY = -2f;
    public float obstacleMaxY = -2f;

    [Header("Coin height")]
    public float coinMinY = -1f;
    public float coinMaxY = 1.5f;

    [Header("Movement")]
    public float objectMoveSpeed = 5f;

    [Header("Coin spawn propability")]
    [Range(0f, 1f)]
    public float coinSpawnChance = 0.65f;

    private float spawnTimer;

    void Start()
    {
        //set the first spawn delay
        SetNextSpawnTime();
    }

    void Update()
    {
        //do not spawn while the game is paused or over
        if (GameManager.Instance == null)
        {
            return;
        }

        if (Time.timeScale == 0f)
        {
            return;
        }

        //only spawn during active game
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnRandomObject();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    void SpawnRandomObject()
    {
        //randomly decide whether to spawn a coin or an obstacle
        bool spawnCoin = Random.value < coinSpawnChance;

        if (spawnCoin)
        {
            SpawnCoin();
        }
        else
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0)
        {
            return;
        }

        //select a random obstacle
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);

        GameObject selectedObstacle = obstaclePrefabs[randomIndex];

        if (selectedObstacle == null)
        {
            return;
        }

        //choose a random vertical position
        float randomY = Random.Range(obstacleMinY, obstacleMaxY);

        Vector3 spawnPosition = new Vector3(spawnPositionX, randomY, 0f);

        //creates new obstacle in scene using the prefab randomly selected
        GameObject obstacle = Instantiate(selectedObstacle, spawnPosition, Quaternion.identity);

        // Set the obstacle movement speed.
        ObstacleMovement movement = obstacle.GetComponent<ObstacleMovement>();

        if (movement != null)
        {
            movement.moveSpeed = objectMoveSpeed;
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab == null)
        {
            return;
        }

        //choose a random coin height
        float randomY = Random.Range(coinMinY, coinMaxY);

        Vector3 spawnPosition = new Vector3(spawnPositionX, randomY, 0f);

        //creates new collectable using coin prefab
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        // Set coin movement speed.
        CoinCollectible movement = coin.GetComponent<CoinCollectible>();

        if (movement != null)
        {
            movement.moveSpeed = objectMoveSpeed;
        }
    }
}