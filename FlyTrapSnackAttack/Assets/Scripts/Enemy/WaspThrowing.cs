using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;

public class WaspThrowing : MonoBehaviour
{
    void Start()
    {
    }

    //placeholder until I think of better movement
    void Update()
    {
        transform.Translate(1 * Time.deltaTime, 0.005f * Mathf.Sin(Time.time*3), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LeftZone") || other.gameObject.CompareTag("RightZone"))
        {
            Destroy(gameObject);
        }
    }
}
