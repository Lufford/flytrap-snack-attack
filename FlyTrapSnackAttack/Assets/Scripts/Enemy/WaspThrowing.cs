using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class WaspThrowing : MonoBehaviour
{

    private int direction = 1;    
    
    void Update()
    {
        
        HandleMovement();
    }

    void HandleMovement()
    {
        transform.Translate(direction * Time.deltaTime, 0.005f * Mathf.Sin(Time.time*3), 0);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "travelPoint")
        {
            int[] dir = {-1, 1};
            direction = dir[Random.Range(0,2)];
        }
        if (other.gameObject.CompareTag("LeftZone") || other.gameObject.CompareTag("RightZone"))
        {
            Destroy(gameObject);
        }
    }
}
