using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float damageInvincibilityLength = .5f;
    private float invincibilityCounter;

    public int currentKevlar;
    public int maxKevlar;

    public GameObject impactEffect;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentKevlar = maxKevlar;

        UIController.instance.healthBar.maxValue = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        UIController.instance.kevlarBar.maxValue = maxKevlar;
        UIController.instance.kevlarBar.value = currentKevlar;
        UIController.instance.kevlarText.text = currentKevlar.ToString() + " / " + maxKevlar.ToString();

    }

    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if(invincibilityCounter <= 0)
            {
                PlayerController.instance.bodySpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }  
    }

    public void DamagePlayer(int damage, bool trueDamage = false)
    {
        if (invincibilityCounter <= 0)
        {

            if (currentKevlar > 0 && trueDamage == false)
            {
                currentKevlar--;

                UIController.instance.kevlarBar.value = currentKevlar;
                UIController.instance.kevlarText.text = currentKevlar.ToString() + " / " + maxKevlar.ToString();
            }
            else
            {
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    PlayerController.instance.gameObject.SetActive(false);

                    UIController.instance.gameOverScreen.SetActive(true);

                    AudioManager.instance.PlayGameOver();
                }

                UIController.instance.healthBar.value = currentHealth;
                UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
            }

            AudioManager.instance.PlaySFX(5);
            Instantiate(impactEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
            Invincibility(damageInvincibilityLength);
        }
    }
    public void Invincibility(float time)
    {
        invincibilityCounter = time;
        PlayerController.instance.bodySpriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
    }

    public void HealPlayer(int value)
    {
        currentHealth += value;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void UpgradeHealth(int value)
    {
        maxHealth += value;
        currentHealth = maxHealth;

        UIController.instance.healthBar.maxValue = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void RepairKevlar(int value)
    {
        currentKevlar += value;

        if (currentKevlar > maxKevlar)
        {
            currentKevlar = maxKevlar;
        }

        UIController.instance.kevlarBar.value = currentKevlar;
        UIController.instance.kevlarText.text = currentKevlar.ToString() + " / " + maxKevlar.ToString();
    }

    public void UpgradeKevlar(int value)
    {
        maxKevlar += value;
        currentKevlar = maxKevlar;

        UIController.instance.kevlarBar.maxValue = maxKevlar;
        UIController.instance.kevlarBar.value = currentKevlar;
        UIController.instance.kevlarText.text = currentKevlar.ToString() + " / " + maxKevlar.ToString();
    }
}
