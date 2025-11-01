using UnityEngine;
public class PotController : MonoBehaviour
{   
    [SerializeField] float speed;
    Rigidbody2D rb;
    private Animator animator;
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
    [SerializeField] private GameObject idleMouth;
    [SerializeField] private Transform rotationPoint;
    
    private AudioSource audioSource;
    public AudioClip audioClip;
    void Awake()=> cam = Camera.main;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            audioSource.PlayOneShot(audioClip, 1f);
            canMove = false;
            idleMouth.SetActive(false);
            Instantiate(tongue, mouth.transform.position, rotationPoint.rotation);
            cooldownTimer = 0;
            moveTimer = 0;
        }
        cooldownTimer += Time.deltaTime;
        moveTimer += Time.deltaTime;
        if (moveTimer > 0.5f)
        {
            canMove = true;
            idleMouth.SetActive(true);
        }

        //Controls if its animating based on movement
        if (movement.x > 0f || movement.x  < 0f) { animator.speed = 1; }
        else { animator.speed = 0; }
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
