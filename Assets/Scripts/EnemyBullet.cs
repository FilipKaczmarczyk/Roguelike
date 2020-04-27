using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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

        if (other.tag == "Player")
        {

        }
        else
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}