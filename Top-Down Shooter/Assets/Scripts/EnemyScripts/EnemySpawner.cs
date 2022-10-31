using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject player;

    //TODO - check if enemy is within bounds

    /// xOffset, yOffset
    public (float, float) minSpawnOffset = (9, 5.5f);
    public (float, float) maxSpawnOffset = (12, 8);

    public int maxEnemyCount = 10;
    /// <summary>
    /// Spawn interval in milliseconds
    /// </summary>
    public const long enemySpawnInterval = 1000;
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
        var spawnOnVerticalAxis = random.Next(0, 2);
        float xOffset, yOffset;
        if (spawnOnVerticalAxis == 1)
        {
            //var playerPosition = GameMode.playerController.gameObject.transform.position;
            xOffset = Random.Range(minSpawnOffset.Item1, maxSpawnOffset.Item1);
            yOffset = Random.Range(0, maxSpawnOffset.Item2);
        }
        else
        {
            xOffset = Random.Range(0, maxSpawnOffset.Item1);
            yOffset = Random.Range(minSpawnOffset.Item2, maxSpawnOffset.Item2);
        }

        var isXNegative = random.Next(0, 2);
        var isYNegative = random.Next(0, 2);

        xOffset = (isXNegative == 0)? xOffset : -xOffset;
        yOffset = (isYNegative == 0) ? yOffset : -yOffset;

        var enemyIndex = random.Next(0, enemies.Count);
        GameObject newEnemy = Instantiate(enemies[enemyIndex]);
        //set position
        newEnemy.transform.position = player.transform.position + new Vector3(xOffset, yOffset, 0);
    }
}
