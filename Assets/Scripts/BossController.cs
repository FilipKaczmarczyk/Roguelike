using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public BossMove[] moves;
    private int currentMove;
    private float moveCounter;

    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D rb;

    public int health;

    public GameObject deathSplatter, hitEffect;
    public GameObject levelExit;

    public BossSequence[] sequences;
    public int currentSequence;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        moves = sequences[currentSequence].moves;

        moveCounter = moves[currentMove].moveLength;

        UIController.instance.bossHealthBar.maxValue = health;
        UIController.instance.bossHealthBar.value = health;
    }

    void Update()
    {
        if(moveCounter > 0)
        {
            moveCounter -= Time.deltaTime;

            moveDirection = Vector2.zero;

            if (moves[currentMove].shouldMove)
            {
                if (moves[currentMove].shouldChasePlayer)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                }

                if (moves[currentMove].shouldPatrol)
                {
                    moveDirection = moves[currentMove].nextPoint.position - transform.position;
                    moveDirection.Normalize();
                }

            }

            rb.velocity = moveDirection * moves[currentMove].moveSpeed;

            if (moves[currentMove].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    shotCounter = moves[currentMove].fireRate;

                    foreach(Transform trans in moves[currentMove].shotPoints)
                    {
                        Instantiate(moves[currentMove].bullet, trans.position, trans.rotation);
                    }
                }
                
            }
        }
        else
        {
            currentMove++;
            if(currentMove >= moves.Length)
            {
                currentMove = 0;
            }

            moveCounter = moves[currentMove].moveLength;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Instantiate(hitEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(deathSplatter, transform.position, transform.rotation);

            if (Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position) < 2f)
            {
                levelExit.transform.position += new Vector3(4f, 0f, 0f);
            }

            levelExit.SetActive(true);

            UIController.instance.bossHealthBar.gameObject.SetActive(false);
        }
        else
        {
            if(health <= sequences[currentSequence].targetHealth && currentSequence < sequences.Length - 1)
            {
                currentSequence++;
                moves = sequences[currentSequence].moves;
                currentMove = 0;
                moveCounter = moves[currentMove].moveLength;
            }
        }

        UIController.instance.bossHealthBar.value = health;
    }
}

[System.Serializable]
public class BossMove
{
    [Header("What is Boss doing")]
    public float moveLength;

    public bool shouldMove;

    public bool shouldChasePlayer;

    public float moveSpeed;

    public bool shouldPatrol;
    public Transform nextPoint;

    public bool shouldShoot;
    public GameObject bullet;
    public float fireRate = 0.4f;
    public Transform[] shotPoints;
}

[System.Serializable]
public class BossSequence
{
    [Header("Sequence")]
    public BossMove[] moves;

    public int targetHealth;
}