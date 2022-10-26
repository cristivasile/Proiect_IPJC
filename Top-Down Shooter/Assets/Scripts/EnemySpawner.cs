using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject meleeEnemy;
    private GameObject rangedEnemy;

    public int maxEnemyCount = 10;
    public float enemySpawnInterval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MeleeEnemy.GetNumberOfInstances() + RangedEnemy.GetNumberOfInstances() < maxEnemyCount)
        {
            Spawn();
        }
    }

    private void Spawn()
    {

    }
}
