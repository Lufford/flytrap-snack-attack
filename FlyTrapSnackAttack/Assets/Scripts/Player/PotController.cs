using UnityEngine;

public class PotController : MonoBehaviour
{   
    [SerializeField] float speed;
    Rigidbody2D rb;
    private Vector3 movement = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LeftZone" || collision.tag == "RightZone")
        {
            transform.position = new Vector2(-transform.position.x - (transform.position.x < 0 ? 0.5f : -0.5f), transform.position.y);
        }
    }
}
