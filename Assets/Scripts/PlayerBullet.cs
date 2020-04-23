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
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(bulettDamage);
        }
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
