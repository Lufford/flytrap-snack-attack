using System;
using UnityEngine;

public class SpearThrown : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private Vector2 moveDirection;

    [SerializeField] private float speed;

    public float damageAmount = 20f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //same thing as the other script but only grabs direction towards the play on spawn so it doesn't track 
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection= (target.position - transform.position).normalized;
        transform.up = moveDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moves towards the fixed direction
        rb.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //destroy itself if it touches "table" tag
        if (other.gameObject.CompareTag("Table"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Energy energy = FindFirstObjectByType<Energy>();
            if (energy != null)
            {
                energy.Damage(damageAmount);
            }

            Destroy(gameObject);
        }
    }
}
