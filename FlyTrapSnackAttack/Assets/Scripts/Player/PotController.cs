using UnityEngine;
public class PotController : MonoBehaviour
{   
    [SerializeField] float speed;
    Rigidbody2D rb;
    private SpriteRenderer mouthSprite;
    private Vector3 movement = Vector3.zero;
    private bool canMove = true;
    float moveTimer = 0;
    
    //Mouth stuff
    private Camera cam;
    private Vector3 mousePos;
    private float cooldownTimer = 2f;
    [SerializeField] private GameObject tongue;
    [SerializeField] private GameObject mouth;
    [SerializeField] private Transform rotationPoint;
    void Awake()=> cam = Camera.main;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mouthSprite = mouth.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (!canMove)
        {
            movement = Vector3.zero;
        }
        else
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePos - rotationPoint.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rotationPoint.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        mouthSprite.color = cooldownTimer>2f ?  new Color(0, 1, 0, 1) : new Color(1, 0, 0, 1);
        
        if (Input.GetMouseButtonDown(0) && cooldownTimer >= 2f)
        {
            canMove = false;
            Instantiate(tongue, mouth.transform.position , rotationPoint.rotation);
            cooldownTimer = 0;
            moveTimer = 0;
        }
        cooldownTimer += Time.deltaTime;
        moveTimer += Time.deltaTime;
        if (moveTimer > 0.5f) canMove = true;
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
