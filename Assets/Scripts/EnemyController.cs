using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public Animator anim;

    public int health = 100;

    public GameObject[] deathSplatters;
    public GameObject onHitEffect;

    public Renderer bodyRenderer;

    void Start()
    {

    }

    void Update()
    {
        // CHOOSE DIRECTION 
        if (bodyRenderer.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            Vector3 playerPos = PlayerController.instance.transform.position;

            if (Vector3.Distance(transform.position, playerPos) < rangeToChasePlayer)
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
}
