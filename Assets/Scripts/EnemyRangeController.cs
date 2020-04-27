using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public float rangeToShootPlayer;

    private Vector3 moveDirection;

    public Animator anim;

    public int health = 100;

    public GameObject[] deathSplatters;
    public GameObject onHitEffect;

    public GameObject bullet;

    public Transform barrel;

    public float fireRate;

    private float fireCounter;

    public Transform gunHand;

    public Renderer bodyRenderer;

    void Start()
    {

    }

    void Update()
    {
        if (bodyRenderer.isVisible)
        {
            // CHOOSE DIRECTION 

            Vector3 playerPos = PlayerController.instance.transform.position;

            if (Vector3.Distance(transform.position, playerPos) < rangeToShootPlayer)
            {
                //moveDirection = playerPos - transform.position;

                if (playerPos.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    gunHand.localScale = new Vector3(-1f, -1f, 1f);
                }
                else
                {
                    transform.localScale = Vector3.one;
                    gunHand.localScale = Vector3.one;
                }

                Vector2 offset = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);
                float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                gunHand.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, barrel.position, barrel.rotation);
                }

            }
            else
            {
                moveDirection = Vector3.zero;
            }

            // MOVEMENT

            moveDirection.Normalize();
            rb.velocity = moveDirection * moveSpeed;

            // ANIMS

            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Instantiate(onHitEffect, transform.position, transform.rotation);

        if(health <= 0)
        {
            Destroy(gameObject);

            Instantiate(deathSplatters[Random.Range(0, deathSplatters.Length)], transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 4) * 90f));
        }
    }

    public void Shoot()
    {
        
    }


}
