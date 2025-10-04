using System;
using UnityEngine;

public class Fly : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tongue"))
        {
            Destroy(gameObject);
            Energy energy = FindFirstObjectByType<Energy>();
            if (energy != null)
            {
                energy.Heal(4);
            }
            GameManager.Instance.updateScore(5);
        }
    }
}
