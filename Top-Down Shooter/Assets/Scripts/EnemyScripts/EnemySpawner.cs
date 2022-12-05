using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject spawnMarker;
    public GameObject player;

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

    // Start is called before the first frame update
    void Start()
    {
        //set player reference
        BaseEnemy.player = player;
    }

    // Update is called once per frame
    void Update()
    {
         lastSpawnInterval += Time.deltaTime * 1000;
        if (lastSpawnInterval > enemySpawnInterval && 
            BaseEnemy.GetNumberOfInstances() < maxEnemyCount)
        {
            lastSpawnInterval = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        System.Random random = new();
        var enemyIndex = random.Next(0, enemies.Count);
        GameObject newEnemy = Instantiate(enemies[enemyIndex]);
        GameObject marker = Instantiate(spawnMarker);

        Vector3 enemyPosition;
        bool positionIsValid = true;
        //set position
        do
        {
            enemyPosition = GetRandomPosition(player.transform.position);
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
        enemy.gameObject.SetActive(false);

        marker.transform.position = position;

        await Task.Delay(spawnDelay);

        Destroy(marker.gameObject);

        enemy.transform.position = position;
        enemy.gameObject.SetActive(true);
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
