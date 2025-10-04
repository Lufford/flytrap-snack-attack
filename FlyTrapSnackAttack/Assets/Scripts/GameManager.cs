using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //enemy prefabs
    [SerializeField] private GameObject[] enemyPrefabs;
    //fly prefab
    [SerializeField] private GameObject flyPrefabs;

    //spawn location
    [SerializeField] private Transform[] spawnLocations;

    private float spawnCooldown = 3f;
    private float spawnTimer = 0;
    private float[] spawnTimes =  {3f,10f,12f};
    private bool gameOver = false;
    private float flySpawnCooldown = 1f;
    private float flySpawnTimer = 0;
    private void Start()
    {

    }

    private void Update()
    {
        if (gameOver) return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnCooldown)
        {
            SpawnEnemy(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)]);
            spawnTimer = 0f;
            spawnCooldown = spawnTimes[Random.Range(0, spawnTimes.Length)];
        }
    
        flySpawnTimer += Time.deltaTime;
        if (flySpawnTimer >= flySpawnCooldown)
        {
            SpawnFly();
            flySpawnTimer = 0f;
        }
        
    }

    public void SetGameOver()
    {
        gameOver = true;
    }

    void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, spawnLocations[Random.Range(0, spawnLocations.Length)].position, Quaternion.identity);
    }

    void SpawnFly()
    {
        Instantiate(flyPrefabs, new Vector3(Random.Range(-8,8),5.7f,0), Quaternion.identity);
    }
}
