using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun gun;

    public float pickupOffset = 1f;
    void Start()
    {

    }

    void Update()
    {
        if (pickupOffset > 0)
        {
            pickupOffset -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && pickupOffset <= 0)
        {
            bool alreadyHave = false;

            foreach(Gun gunToCheck in PlayerController.instance.availableGuns)
            {
                if(gunToCheck.weaponName == gun.weaponName)
                {
                    alreadyHave = true;
                }
            }

            if (!alreadyHave)
            {
                Gun newGun = Instantiate(gun);
                newGun.transform.parent = PlayerController.instance.gunHand;
                newGun.transform.position = PlayerController.instance.gunHand.position;
                newGun.transform.localRotation = Quaternion.Euler(Vector3.zero);
                newGun.transform.localScale = Vector3.one;

                PlayerController.instance.availableGuns.Add(newGun);
                PlayerController.instance.gunInUse = PlayerController.instance.availableGuns.Count - 1;

                PlayerController.instance.ChangeGun();
            }

            AudioManager.instance.PlaySFX(2);
            Destroy(gameObject);
        }
    }
}
