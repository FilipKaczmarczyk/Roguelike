using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject message;

    private bool inZone;

    public bool isHealthRestore, isHealthUpgrade, isKevlarRestore, isKevlarUpgrade, isWeapon;

    public int itemCost;

    public int healthUpgradeValue;

    public int kevlarUpgradeValue;

    public Gun[] potencialGuns;
    private Gun selectedGun;
    public SpriteRenderer gunSprite;
    public Text info;


    void Start()
    {
        if (isWeapon)
        {
            int randomGun = Random.Range(0, potencialGuns.Length);
            selectedGun = potencialGuns[randomGun];

            gunSprite.sprite = selectedGun.gunImage;
            info.text = "Buy " + selectedGun.weaponName + "\n - " + selectedGun.price + " Gold -";
            itemCost = selectedGun.price;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inZone)
        {
            if(LevelManager.instance.currentCoins >= itemCost)
            {
                LevelManager.instance.SpendCoins(itemCost);

                AudioManager.instance.PlaySFX(8);

                if (isHealthRestore)
                {
                    PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                }

                if (isHealthUpgrade)
                {
                    PlayerHealthController.instance.UpgradeHealth(healthUpgradeValue);
                }

                if (isKevlarRestore)
                {
                    PlayerHealthController.instance.RepairKevlar(PlayerHealthController.instance.maxKevlar);
                }

                if (isKevlarUpgrade)
                {
                    PlayerHealthController.instance.UpgradeKevlar(kevlarUpgradeValue);
                }

                if (isWeapon)
                {
                    Gun newGun = Instantiate(selectedGun);
                    newGun.transform.parent = PlayerController.instance.gunHand;
                    newGun.transform.position = PlayerController.instance.gunHand.position;
                    newGun.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    newGun.transform.localScale = Vector3.one;

                    PlayerController.instance.availableGuns.Add(newGun);
                    PlayerController.instance.gunInUse = PlayerController.instance.availableGuns.Count - 1;

                    PlayerController.instance.ChangeGun();
                }

                gameObject.SetActive(false);
                inZone = false;
            }
            else
            {
                AudioManager.instance.PlaySFX(9);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            message.SetActive(true);

            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.SetActive(false);

            inZone = false;
        }
    }
}
