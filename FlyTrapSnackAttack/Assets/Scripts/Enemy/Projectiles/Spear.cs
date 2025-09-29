using UnityEngine;
using Random = UnityEngine.Random;

public class Spear : MonoBehaviour
{
    private Transform target;
    private bool hasSpear = true;
    private bool isTracking = true;
    private float trackCooldown;
    private int numSpears;
    private float timer;
    [SerializeField] private GameObject prefab;

    //This is for the spear child attached to the wasp. It only controls
    //the appearance of tracking the player
    //the delay before throwing a spear
    //how many spears can be thrown
    void Start()
    {
        //assigns target to the position of the game object with "player" tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //chooses random int between 3-5, 1-2
        trackCooldown = Random.Range(3, 6);
        numSpears = Random.Range(1,4);
    }

    private void Update()
    {
        //increments time to check if it has met the cooldown
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ThrowSpear();
    }

    private void ThrowSpear()
    {
        //if timer is less than cooldown and numSpear isn't 0, keep tracking the player
        if (timer <= trackCooldown && hasSpear)
        {
            //subtract the two coordinates to find the vector then normalize it 
            Vector2 direction = (target.position - transform.position).normalized;
            //changes the direction of the spear to face the player
            transform.up = direction;
        }
        else
        {
            //once cooldown is over, stop tracking
            isTracking = false;
        }
        //once stopped tracking and has spears left, instantiate spear, reduce spears by 1, reset timer, start tracking again
        if (!isTracking && hasSpear)
        {
            timer = 0;
            Instantiate(prefab, transform.position, Quaternion.identity);
            numSpears--;
            isTracking = true;
        }
        //no spears left
        if (numSpears == 0)
        {
            hasSpear = false;
        }
    }
}
