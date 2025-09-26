using UnityEngine;

public class BeetleBomber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(1 * Time.deltaTime, //x value - horizontal speed
        0.002f * Mathf.Sin(Time.time*1), //y value - vertical speed
        0); // z speed
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LeftZone") || other.gameObject.CompareTag("RightZone"))
        {
            Destroy(gameObject);
        }
    }
}
