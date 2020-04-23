using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public Animator anim;

    public int health = 100;

    void Start()
    {
        
    }

    void Update()
    {
        // CHOOSE DIRECTION 

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;

            if (PlayerController.instance.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
