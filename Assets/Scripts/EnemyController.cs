using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    private Vector3 moveDirection;

    [Header("Chase Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;

    [Header("Run Away")]
    public bool shouldRunAway;
    public float rangeToRunAway;

    [Header("Wander")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    [Header("Patrol")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    public bool drop;
    public GameObject[] drops;
    public float itemDropPercent;

    public Animator anim;

    public int health = 100;

    public GameObject[] deathSplatters;
    public GameObject onHitEffect;

    public Renderer bodyRenderer;

    void Start()
    {
        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.5f);
        }
    }

    void Update()
    {
        // CHOOSE DIRECTION 
        if (bodyRenderer.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;
            Vector3 playerPos = PlayerController.instance.transform.position;

            if (Vector3.Distance(transform.position, playerPos) < rangeToChasePlayer && shouldChasePlayer)
            {

                moveDirection = playerPos - transform.position;

                if (playerPos.x < transform.position.x)
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
                if (shouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        moveDirection = wanderDirection;

                        if(wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.5f);
                        }

                        if (wanderDirection.x <= 0f)
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else
                        {
                            transform.localScale = Vector3.one;
                        }


                    }
                    if(pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if(pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * 0.75f, wanderLength * 1.5f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), 0);
                        }
                    }
                }

                if (shouldPatrol)
                {
                    moveDirection = patrolPoints[currentPatrolPoint].transform.position - transform.position;

                    if (moveDirection.x <= 0f)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    else
                    {
                        transform.localScale = Vector3.one;
                    }

                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .1f)
                    {
                        currentPatrolPoint++;
                        if(currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                    }
                }
            }

            if (Vector3.Distance(transform.position, playerPos) < rangeToRunAway && shouldRunAway)
            {
                moveDirection = transform.position - playerPos;   
            }

                /*else
                {
                    moveDirection = Vector3.zero;
                }
                */


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

        AudioManager.instance.PlaySFX(4);

        Instantiate(onHitEffect, transform.position, transform.rotation);

        if(health <= 0)
        {
            Destroy(gameObject);

            Instantiate(deathSplatters[Random.Range(0, deathSplatters.Length)], transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 4) * 90f));

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if((transform.position.x < -7.7 || transform.position.x > 7.7) && (transform.position.y < -3.5 || transform.position.y > 3.6))
        {
            wanderDirection = new Vector3(-wanderDirection.x, -wanderDirection.y, 0);
        }
        else if(transform.position.x < -7.7 || transform.position.x > 7.7)
        {
            wanderDirection = new Vector3(-wanderDirection.x, wanderDirection.y, 0);
        }
        else if (transform.position.y < -3.5 || transform.position.y > 3.6)
        {
            wanderDirection = new Vector3(wanderDirection.x, -wanderDirection.y, 0);
        }
    }
}
