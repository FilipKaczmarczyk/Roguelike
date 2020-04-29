using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 50;

    public float pickupOffset = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        if(pickupOffset > 0)
        {
            pickupOffset -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && pickupOffset <= 0)
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}


