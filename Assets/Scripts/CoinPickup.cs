using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    public float pickupOffset = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
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
            LevelManager.instance.GetCoins(coinValue * Random.Range(1,11));
            AudioManager.instance.PlaySFX(7);
            Destroy(gameObject);
        }
    }
}
