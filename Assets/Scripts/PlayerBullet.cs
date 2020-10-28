using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb;

    public GameObject impactEffect;

    public int bulettDamage = 25;

    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        Destroy(gameObject);

        Instantiate(impactEffect, transform.position, transform.rotation);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(bulettDamage);
        }
        else if (other.tag == "EnemyRange")
        {
            other.GetComponent<EnemyRangeController>().TakeDamage(bulettDamage);
        }
        else if(other.tag == "Boss")
        {
            other.GetComponent<BossController>().TakeDamage(bulettDamage);
        }
       /* else
        {
            
        }*/
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
