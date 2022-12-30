using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{   private enum State
    {
        IDLE,     //before the game starts
        SPAWNING, //in the process of spawning enemies
        WAITING,  //waiting for the player to kill the enemies
        COUNTING, //counting down after a wave ends
    }

    public List<GameObject> enemyPrefabs;
    public GameObject spawnMarkerPrefab;
    public Transform player;
    public WaveNotifier waveNotifier;
    public TimerNotifier timerNotifier;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject topBound;
    public GameObject bottomBound;

    [SerializeField]
    private UnityEvent openShopEvent;

    public static Vector2 minSpawnOffset = new(2f, 5.5f);
    public static Vector2 maxSpawnOffset = new(2f, 8f);

    public int baseEnemyCount = 8;
    public const float enemySpawnInterval = 0.25f;
    //time elapsed between marker and enemy appearances
    private const float markerToSpawnInterval = 0.5f;

    /// <summary>
    /// Wave spawner variables
    /// </summary>
    //delay between player spawn and first wave
    public float firstWaveDelay = 1.5f;
    //delay between waves in seconds
    public float waveInterval = 2.0f;
    //max wave duration
    public float waveDuration = 30.0f;
    public float remainingWaveDuration = 0.0f;
    private float timeSinceWaveEnded = 0.0f;
    private int currentWave = 1;
    public int maxWave = 20;
    public float difficultyModifier = 1.0f;
    public float difficultyIncrease = 0.15f; //increases by x% each wave
    /// <summary>
    /// Eenemies will spawn in bursts every <burstInterval> seconds. Every <burstIncreaseInterval> waves the nr of bursts will increment
    /// </summary>
    public int burstNumber = 2;
    public float burstInterval = 5.0f;
    public int burstIncreaseInterval = 3;

    private State spawnerState = State.IDLE;

    private IEnumerator SpawnFirstWave()
    {
        yield return new WaitForSeconds(firstWaveDelay);
        yield return SpawnWave();
    }

    private void Start()
    {
        waveNotifier.SetWave(currentWave, maxWave);
        StartCoroutine(SpawnFirstWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerState != State.IDLE)
        {
            remainingWaveDuration -= Time.deltaTime;
        }
        timerNotifier.UpdateText(remainingWaveDuration);

        //waiting for the player to kill the enemies
        if(spawnerState == State.WAITING)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Count() == 0 || remainingWaveDuration - waveInterval <= 0.0f)   
            //wave is completed, start counting down
            {
                spawnerState = State.COUNTING;
                timeSinceWaveEnded = 0.0f;
                remainingWaveDuration = waveInterval;
            }
        }
        if(spawnerState == State.COUNTING)
        {
            timeSinceWaveEnded += Time.deltaTime;
            //if last wave has ended more than waveInterval seconds ago
            if(timeSinceWaveEnded > waveInterval)
            {
                //interval has passed, start next wave
                currentWave += 1;
                if (currentWave <= maxWave)
                {
                    openShopEvent.Invoke();

                    if ((currentWave - 1) % burstIncreaseInterval == 0)
                        burstNumber += 1;

                    //notify wave UI
                    waveNotifier.SetWave(currentWave, maxWave);

                    //modify difficulty and start new wave
                    StartCoroutine(SpawnWave());
                }
                else
                {
                    //TODO - game end UI
                    Time.timeScale = 0;
                }
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        spawnerState = State.SPAWNING;
        waveDuration = 20.0f + burstNumber * burstInterval;
        remainingWaveDuration = waveDuration;
        difficultyModifier += difficultyModifier * difficultyIncrease;

        for (int burstIndex = 0; burstIndex < burstNumber; burstIndex++)
        {
            for (int enemyIndex = 0; enemyIndex < baseEnemyCount * difficultyModifier; enemyIndex++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(enemySpawnInterval);
            }
            yield return new WaitForSeconds(burstInterval);
        }

        spawnerState = State.WAITING;

    }
    private void SpawnEnemy()
    {
        IEnumerator SpawnWithMarker(GameObject enemyToSpawn, Vector3 spawnLocation)
        {
            var marker = Instantiate(spawnMarkerPrefab, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(markerToSpawnInterval);
            Destroy(marker);

            var enemy = Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
            enemy.GetComponent<Enemy>().ModifyStats(difficultyModifier);
            enemy.GetComponent<EnemyAI>().player = player;
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

        StartCoroutine(SpawnWithMarker(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], enemyPosition));
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
