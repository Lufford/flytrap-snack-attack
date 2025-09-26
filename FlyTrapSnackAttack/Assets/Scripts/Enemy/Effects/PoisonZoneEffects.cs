using UnityEngine;

public class PoisonZoneEffects : MonoBehaviour
{

    private float duration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //random duration for the poison/stink zone to last on table
        duration = Random.Range(3f, 6f);

        //Destroy object after the duration
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //I dont know if the poison would need to hurt player here or in player script
    // Add poison damage here if needed
}
