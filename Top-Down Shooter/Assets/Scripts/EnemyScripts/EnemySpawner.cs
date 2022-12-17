using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public GameObject spawnMarkerPrefab;
    public Transform player;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject topBound;
    public GameObject bottomBound;

    /// xOffset, yOffset
    public static Vector2 minSpawnOffset = new(2f, 5.5f);
    public static Vector2 maxSpawnOffset = new(2f, 8f);

    public int maxEnemyCount = 10;
    /// <summary>
    /// Spawn interval in milliseconds
    /// </summary>
    public const int enemySpawnInterval = 750;

    /// <summary>
    /// Time elapsed before the spawn marker dissapears and an enemy appears
    /// </summary>
    public const int spawnDelay = 1500;
    /// <summary>
    /// Ms elapsed since last enemy was spawned
    /// </summary>
    float lastSpawnInterval = enemySpawnInterval + 1;

    // Update is called once per frame
    void Update()
    {
         lastSpawnInterval += Time.deltaTime * 1000;
        if (lastSpawnInterval > enemySpawnInterval && 
            Enemy.GetNumberOfInstances() < maxEnemyCount)
        {
            lastSpawnInterval = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        System.Random random = new();
        var enemyIndex = random.Next(0, enemyPrefabs.Count);
        GameObject newEnemy = Instantiate(enemyPrefabs[enemyIndex]);
        newEnemy.GetComponent<EnemyAI>().player = player;
        GameObject marker = Instantiate(spawnMarkerPrefab);

        Vector3 enemyPosition;
        bool positionIsValid = true;
        //set position
        do
        {
            enemyPosition = GetRandomPosition(player.position);
            //check if the position is valid
            if (leftBound != null && rightBound != null && topBound != null && bottomBound != null) 
                //only check if the bounds are defined to prevent an infinite loop
                positionIsValid = (enemyPosition.x > leftBound.transform.position.x
                && enemyPosition.x < rightBound.transform.position.x
                && enemyPosition.y > bottomBound.transform.position.y
                && enemyPosition.y < topBound.transform.position.y);
        } while (!positionIsValid);

        CreateMarkerAndSpawn(newEnemy, marker, enemyPosition);
    }

    private async void CreateMarkerAndSpawn(GameObject enemy, GameObject marker, Vector3 position)
    {
        enemy.SetActive(false);

        marker.transform.position = position;

        await Task.Delay(spawnDelay);

        Destroy(marker);

        enemy.transform.position = position;
        enemy.SetActive(true);
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
