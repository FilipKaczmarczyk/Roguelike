using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;

    public GameObject impactEffect;

    public int bulettDamage = 25;

    private Vector3 direction;

    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(bulettDamage);
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