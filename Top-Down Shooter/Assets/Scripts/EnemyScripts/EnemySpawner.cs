using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public GameObject spawnMarkerPrefab;
    public Transform player;
    public WaveNotifier waveNotifier;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject topBound;
    public GameObject bottomBound;

    public static Vector2 minSpawnOffset = new(2f, 5.5f);
    public static Vector2 maxSpawnOffset = new(2f, 8f);

    public int maxEnemyCount = 10;
    public const float enemySpawnInterval = 0.75f;

    public const float spawnDelay = 1.5f;
    float lastSpawnTime;

    public int currentWave = 1;
    public int maxWave = 20;

    private void Start()
    {
        waveNotifier.SetWave(currentWave, maxWave);
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawnTime += Time.deltaTime;
        if (lastSpawnTime >= enemySpawnInterval && Enemy.GetNumberOfInstances() < maxEnemyCount)
        {
            lastSpawnTime = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        IEnumerator Spawn(float delay, GameObject enemyToSpawn, GameObject markerPrefab, Vector3 spawnLocation)
        {
            var marker = Instantiate(markerPrefab, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(delay);
            Destroy(marker);

            var enemy = Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
            enemy.GetComponent<EnemyAI>().player = player;

            currentWave++;
            waveNotifier.SetWave(currentWave, maxWave);
        }

        Vector3 enemyPosition;
        bool positionIsValid = true;
        //set position
        do
        {
            enemyPosition = GetRandomPosition(player.position);
            //check if the position is valid
            if (leftBound != null && rightBound != null && topBound != null && bottomBound != null) 
                //only check if the bounds are defined to prevent an infinite loop
                positionIsValid = (
                   enemyPosition.x > leftBound.transform.position.x
                && enemyPosition.x < rightBound.transform.position.x
                && enemyPosition.y > bottomBound.transform.position.y
                && enemyPosition.y < topBound.transform.position.y);
        } while (!positionIsValid);

        StartCoroutine(Spawn(spawnDelay, enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnMarkerPrefab, enemyPosition));
    }

    private static Vector3 GetRandomPosition(Vector3 playerPosition)
    {
        System.Random random = new();
        var spawnOnVerticalAxis = random.Next(0, 2);
        float xOffset, yOffset;
        if (spawnOnVerticalAxis == 1)
        {
            //var playerPosition = GameMode.playerController.gameObject.transform.position;
            xOffset = Random.Range(minSpawnOffset.x, maxSpawnOffset.x);
            yOffset = Random.Range(0, maxSpawnOffset.y);
        }
        else
        {
            xOffset = Random.Range(0, maxSpawnOffset.x);
            yOffset = Random.Range(minSpawnOffset.y, maxSpawnOffset.y);
        }

        var isXNegative = random.Next(0, 2);
        var isYNegative = random.Next(0, 2);

        xOffset = (isXNegative == 0) ? xOffset : -xOffset;
        yOffset = (isYNegative == 0) ? yOffset : -yOffset;

        return playerPosition + new Vector3(xOffset, yOffset);
    }
}
