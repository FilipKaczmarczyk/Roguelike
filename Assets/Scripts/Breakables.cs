using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
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

                for(int i = 0; i < brokenPieces.Length; i++)
                {
                    Instantiate(brokenPieces[i], transform.position, transform.rotation);
                }
            }
        }
        else if(other.tag == "Bullet")
        {
            Destroy(gameObject);

            for (int i = 0; i < brokenPieces.Length; i++)
            {
                Instantiate(brokenPieces[i], transform.position, transform.rotation);
            }
        }
    }
}
