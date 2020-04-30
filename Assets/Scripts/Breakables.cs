using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;

    public bool drop;
    public GameObject[] drops;
    public float itemDropPercent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(PlayerController.instance.dashCounter > 0)
            {
                Destroy(gameObject);
                Broken();
            }
        }
        else if(other.tag == "Bullet")
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(0);

            Broken();
        }
    }

    public void Broken()
    {
        for (int i = 0; i < brokenPieces.Length; i++)
        {
            Instantiate(brokenPieces[i], transform.position, transform.rotation);
        }

        if (drop)
        {
            float dropChance = Random.Range(0f, 100f);

            if (dropChance < itemDropPercent)
            {
                int randomItem = Random.Range(0, drops.Length);
                Instantiate(drops[randomItem], transform.position, transform.rotation);
            }
        }
    }
}
