using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 5f;
    private Vector2 moveDirection;
    private float currentMoveSpeed;

    public float dashSpeed = 8f;
    public float dashLength = .5f;
    public float dashCooldown = 1.5f;
    public float dashInvincibility = .5f;

    [HideInInspector]
    public float dashCounter = 0f;
    private float dashCooldownCounter = 0f;

    [HideInInspector]
    public bool canMove = true;

    public Rigidbody2D rb;

    public Transform gunHand;

    public Animator anim;

    public SpriteRenderer bodySpriteRenderer;

    public List<Gun> availableGuns = new List<Gun>();

    public Gun[] potencialGuns;

    [HideInInspector]
    public int gunInUse;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentMoveSpeed = moveSpeed;

        if(Save.instance.load == true)
        {
            foreach(string weaponName in Save.instance.availableGunsNames )
            {
                for(int i = 0; i < potencialGuns.Length; i++)
                {
                    if(weaponName == potencialGuns[i].weaponName)
                    {
                        Gun newGun = Instantiate(potencialGuns[i]);
                        newGun.transform.parent = gunHand;
                        newGun.transform.position = gunHand.position;
                        newGun.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        newGun.transform.localScale = Vector3.one;

                        availableGuns.Add(newGun);
                        gunInUse = availableGuns.Count - 1;
                    }
                }
            }

            ChangeGun();
        }

        UIController.instance.GunInUseImage.sprite = availableGuns[gunInUse].gunImage;
    }

    void Update()
    {
        if (canMove == true && LevelManager.instance.isPause == false)
        {
            // Movement

            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");

            moveDirection.Normalize();

            rb.velocity = moveDirection * currentMoveSpeed;

            // Rotation

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            if (mousePos.x < screenPoint.x)
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

            // Anims

            if (moveDirection != Vector2.zero)
            {
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
            }

            // Change Weapon

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(availableGuns.Count > 0)
                {
                    gunInUse++;
                    if(gunInUse >= availableGuns.Count)
                    {
                        gunInUse = 0;
                    }

                    ChangeGun();
                }
                else
                {
                    Debug.LogError("No guns available");
                }
            }

            // Dashing

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCounter <= 0 && dashCooldownCounter <= 0)
                {
                    currentMoveSpeed = dashSpeed;
                    dashCounter = dashLength;

                    anim.SetTrigger("dash");
                    AudioManager.instance.PlaySFX(3);
                    PlayerHealthController.instance.Invincibility(dashInvincibility);
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    currentMoveSpeed = moveSpeed;
                    dashCooldownCounter = dashCooldown;
                }
            }

            if (dashCooldownCounter > 0)
            {
                dashCooldownCounter -= Time.deltaTime;
            }

            
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("moving", false);
        }
    }

    public void ChangeGun()
    {
        foreach(Gun gun in availableGuns)
        {
            gun.gameObject.SetActive(false);
        }

        availableGuns[gunInUse].gameObject.SetActive(true);
        UIController.instance.GunInUseImage.sprite = availableGuns[gunInUse].gunImage;
    }
}
