using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 5f;
    private Vector2 moveDirection;

    public Rigidbody2D rb;

    public Transform gunHand;

    private Camera cam;

    public Animator anim;

    public GameObject bullet;

    public Transform barrel;

    public float rateOfFire = 100f;

    private bool canShoot = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // MOVEMENT

        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        moveDirection.Normalize();

        rb.velocity = moveDirection * moveSpeed;

        // ROTATION

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunHand.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunHand.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunHand.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

        if ((Input.GetMouseButtonDown(0) && canShoot) 
            || (Input.GetMouseButton(0) && canShoot))
        {
            Instantiate(bullet, barrel.position, barrel.rotation);
            canShoot = false;
            StartCoroutine(NextBullet());
        }

        // ANIMS

        if(moveDirection != Vector2.zero)
        {
            anim.SetBool("moving", true); 
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }
    IEnumerator NextBullet()
    {
        yield return new WaitForSeconds((float)60 / rateOfFire);
        canShoot = true;
    }

}
