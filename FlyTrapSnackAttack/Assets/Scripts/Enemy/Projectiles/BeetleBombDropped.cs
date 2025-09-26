using UnityEngine;
using System;

public class BeetleBombDropped : MonoBehaviour
{

    
    
    private Rigidbody2D rb;
    [SerializeField] public float speed;
    [SerializeField] private GameObject PoisonZone; //prefab?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

        private void OnCollisionEnter2D(Collision2D other)
    {
        //"blow up" when it hits the table
        if (other.gameObject.CompareTag("Table"))
        {

            // blow up the bomb & create poison / "stink" zone
            Instantiate(PoisonZone, transform.position, Quaternion.identity);

            //destroy after bomb is instantiated
            Destroy(gameObject);
        }
    }
}
