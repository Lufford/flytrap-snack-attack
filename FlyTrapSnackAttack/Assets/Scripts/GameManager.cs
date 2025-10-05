using System;
using System.Collections;
using TMPro;
using Unity.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        SceneManager.sceneLoaded += onSceneLoad;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= onSceneLoad;
    }

    void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        Debug.Log(mode.ToString());
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
        if (scene.name.Equals("TitleScreen") || scene.name.Equals("GameOverScreen"))
        {
            gameOver = true;
        }
        else
        {
            gameOver = false;
        }
    }


    //enemy prefabs
    [SerializeField] private GameObject[] enemyPrefabs;
    //fly prefab
    [SerializeField] private GameObject flyPrefabs;

    //spawn location
    private GameObject[] spawnLocations;

    //Scoreboard
    [SerializeField] private GameObject scoreboard;

    [SerializeField] private int score = 0;

    private float spawnCooldown = 3f;
    private float spawnTimer = 0;
    private float[] spawnTimes =  {3f,10f,12f};
    private bool gameOver = false;
    private float flySpawnCooldown = 1f;
    private float flySpawnTimer = 0;
    private void Start()
    {
        StartCoroutine(FindScoreBoard());
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
        SceneManager.LoadScene("GameOverScreen");
        StartCoroutine(FindScoreBoard());
    }

    public void startGame()
    {
        gameOver = false;
        score = 0;
        StartCoroutine(FindScoreBoard());
    }

    public void updateScore(int score)
    {
        this.score += score;
        scoreboard.GetComponent<TMP_Text>().text = "Score: " + this.score.ToString();
    }

    void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
    }

    void SpawnFly()
    {
        Instantiate(flyPrefabs, new Vector3(Random.Range(-8,8),5.7f,0), Quaternion.identity);
    }

    private IEnumerator FindScoreBoard()
    {
        yield return new WaitForSeconds(0.1f);
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard");
        if (gameOver)
        {
            scoreboard.GetComponent<TMP_Text>().text = "Final Score " + score.ToString();
        }
    }
}
