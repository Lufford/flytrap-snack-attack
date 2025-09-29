using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeetleBomber : MonoBehaviour
{


    private bool hasBomb = true;
    private float trackCooldown;
    private int bombNum;
    private float timer;
    [SerializeField] private GameObject BeetleBomb; //prefab?


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bombNum = 1; //only one bomb can be dropped
        trackCooldown = Random.Range(2f, 15f); //random cooldown between 2 and 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(1 * Time.deltaTime, //x value - horizontal speed
        0.002f * Mathf.Sin(Time.time * 1), //y value - vertical speed
        0); // z speed
        
        //increments time to check if it has met the cooldown for bomb drop
        timer += Time.deltaTime;
    }
    
    private void FixedUpdate()
    {
        DropBomb();
    }

    private void DropBomb()
    {
        //if it has a bomb and the timer is over 2 seconds, drop the bomb
        if (hasBomb && timer >= trackCooldown)
        {
            timer = 0;
            Instantiate(BeetleBomb, transform.position, Quaternion.identity);
            bombNum--;
            hasBomb = false; //only one bomb can be dropped
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LeftZone") || other.gameObject.CompareTag("RightZone"))
        {
            Destroy(gameObject);
        }
    }
}
