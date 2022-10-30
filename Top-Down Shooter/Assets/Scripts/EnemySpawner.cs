using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject meleeEnemy;
    private GameObject rangedEnemy;

    public int maxEnemyCount = 10;
    /// <summary>
    /// Spawn interval in milliseconds
    /// </summary>
    public long enemySpawnInterval = 100;
    private long lastSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (lastSpawn + enemySpawnInterval < currentTime && 
            MeleeEnemy.GetNumberOfInstances() + RangedEnemy.GetNumberOfInstances() < maxEnemyCount)
        {
            lastSpawn = currentTime;
            Spawn();
        }
    }

    private void Spawn()
    {
        //var playerPosition = GameMode.playerController.gameObject.transform.position;

        float xOffset = 0;
        float yOffset = 0;
        System.Random rnd = new();
        var randomResult = rnd.Next(0, 4);

        
    }
}
