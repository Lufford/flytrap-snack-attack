using System;
using System.Collections;
using TMPro;
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
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
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

            StartCoroutine(FindScoreBoard());
            StartCoroutine(FindTimer());
        }
        level = scene.buildIndex;
    }


    //enemy prefabs
    [SerializeField] private GameObject[] enemyPrefabs;
    //fly prefab
    [SerializeField] private GameObject flyPrefabs;

    //spawn location
    private GameObject[] spawnLocations;

    //Scoreboard
    private GameObject scoreboard;
    [SerializeField] private int score = 0;

    //Timer
    private GameObject timer;
    [SerializeField] private float time = 120f;

    private float spawnCooldown = 3f;
    private float spawnTimer = 0;
    private float[] spawnTimes =  {3f,10f,12f};
    private float flySpawnCooldown = 1f;
    private float flySpawnTimer = 0;
    private bool gameOver = false;
    private bool levelTransitioning = false;
    private int level = 0;

    private void Start()
    {
        Debug.Log(level);
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

        if (!gameOver && timer != null && !levelTransitioning)
        {
            UpdateTimer();
        }

        if (time <= 0f && !levelTransitioning && !gameOver)
        {
            StartCoroutine(LevelChange());
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
        SceneManager.LoadScene("GameOverScreen");
        StartCoroutine(FindScoreBoard());
    }

    public void StartGame()
    {
        gameOver = false;
        score = 0;
        StartCoroutine(FindScoreBoard());
        StartCoroutine(FindTimer());
    }

    public void updateScore(int score)
    {
        this.score += score;
        scoreboard.GetComponent<TMP_Text>().text = "Score: " + this.score.ToString();
    }
    void UpdateTimer()
    {
        time -= Time.deltaTime;
        timer.GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(time).ToString(@"m\:ss");
    }

    void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
    }

    void SpawnFly()
    {
        Instantiate(flyPrefabs, new Vector3(Random.Range(-8,8),5.7f,0), Quaternion.identity);
    }

    //allows you to change the level
    private IEnumerator LevelChange()
    {
        levelTransitioning = true;
        level++;
        //probably need something to become active here to show how well you did in each level before it starts the next one

        //allows you to determine the timer for each level, and other things you may want to change in between
        switch (level)
        {
            case 1:
                time = 120f;
                break;
            case 2:
                break;
            case 3:
                break;
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(level);
        levelTransitioning = false;

        //assuming we don't have these set as don't destroy on load, we will need to find them each new level
        //as the score is stored in the gamemanager this won't get rid of your score between levels
        StartCoroutine(FindScoreBoard());
        StartCoroutine(FindTimer());
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

    private IEnumerator FindTimer()
    {
        yield return new WaitForSeconds(0.1f);
        timer = GameObject.FindGameObjectWithTag("Timer");
    }
}
