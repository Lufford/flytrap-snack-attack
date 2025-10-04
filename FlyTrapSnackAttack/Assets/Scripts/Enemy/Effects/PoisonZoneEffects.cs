using UnityEngine;

public class PoisonZoneEffects : MonoBehaviour
{

    private float duration;
    public float damageAmount = 10f;

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

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Energy energy = FindFirstObjectByType<Energy>();
            if (energy != null)
            {
                energy.Damage(damageAmount);
            }

            
        }
    }
}
