using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject message;

    private bool inZone;

    public bool isHealthRestore, isHealthUpgrade, isKevlarRestore, isKevlarUpgrade, isWeapon;

    public int itemCost;

    public int healthUpgradeValue;

    public int kevlarUpgradeValue;

    void Start()
    {
        
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
