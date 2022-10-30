using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;

    public GameObject player;

    public float maxSpawnOffset = 8;
    public float minSpawnOffset = 5.5f;
    public int maxEnemyCount = 10;
    /// <summary>
    /// Spawn interval in milliseconds
    /// </summary>
    public long enemySpawnInterval = 1000;
    /// <summary>
    /// Ms elapsed since last enemy was spawned
    /// </summary>
    float lastSpawnInterval;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawnInterval += Time.deltaTime * 1000;
        if (lastSpawnInterval > enemySpawnInterval && 
            MeleeEnemy.GetNumberOfInstances() + RangedEnemy.GetNumberOfInstances() < maxEnemyCount)
        {
            lastSpawnInterval = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        //var playerPosition = GameMode.playerController.gameObject.transform.position;
        var xOffset = Random.Range(minSpawnOffset, maxSpawnOffset);
        var yOffset = Random.Range(minSpawnOffset, maxSpawnOffset);

        System.Random random = new();
        var isXNegative = random.Next(0, 2);
        var isYNegative = random.Next(0, 2);

        xOffset = (isXNegative == 0)? xOffset : -xOffset;
        yOffset = (isYNegative == 0) ? yOffset : -yOffset;

        var enemyIndex = random.Next(0, enemies.Count);
        GameObject newEnemy = Instantiate(enemies[enemyIndex]);
        newEnemy.transform.position = player.transform.position + new Vector3(xOffset, yOffset, 0);
        //GameObject newEnemy = Instantiate(meleeEnemy);
        //newEnemy = Instantiate(rangedEnemy) as GameObject;
    }
}
