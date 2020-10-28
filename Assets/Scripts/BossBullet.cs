using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 7f;

    public GameObject impactEffect;

    public int bulettDamage = 25;

    private Vector3 direction;

    void Start()
    {
        direction = transform.right;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (!BossController.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
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
