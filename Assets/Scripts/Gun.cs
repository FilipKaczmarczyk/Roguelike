using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;

    public Transform barrel;

    public float fireRate;
    private float shotCounter = 0;

    private bool canShoot = true;

    public bool automatic;

    public string weaponName;
    public Sprite gunImageUI;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPause)
        {

            if(shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if ((Input.GetMouseButtonDown(0) && canShoot)|| (Input.GetMouseButton(0) && canShoot && automatic))
                {
                    Instantiate(bullet, barrel.position, barrel.rotation);
                    shotCounter = (1 / fireRate);
                    switch (PlayerController.instance.availableGuns[PlayerController.instance.gunInUse].name)
                    {
                        case "Pistol":
                            AudioManager.instance.PlaySFX(1);
                            break;
                        case "Rifle":
                            AudioManager.instance.PlaySFX(11);
                            break;
                        case "Magnum":
                            AudioManager.instance.PlaySFX(10);
                            break;
                        case "Shotgun":
                            AudioManager.instance.PlaySFX(12);
                            break;
                        case "Uzi":
                            AudioManager.instance.PlaySFX(13);
                            break;
                        case "Minigun":
                            AudioManager.instance.PlaySFX(14);
                            break;
                    }
                }
            }
        }
    }
}
