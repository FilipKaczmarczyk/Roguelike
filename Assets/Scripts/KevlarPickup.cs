using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KevlarPickup : MonoBehaviour
{
    public int kevlarAmount = 2;

    public float pickupOffset = 1f;
    void Start()
    {

    }

    void Update()
    {
        if (pickupOffset > 0)
        {
            pickupOffset -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && pickupOffset <= 0)
        {
            AudioManager.instance.PlaySFX(2);
            PlayerHealthController.instance.RepairKevlar(kevlarAmount);
            Destroy(gameObject);
        }
    }
}
