using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

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
        
    }

    public void DamagePlayer(int damage)
    {
        if(currentKevlar > 0)
        {
            currentKevlar --;

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
            }

            UIController.instance.healthBar.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }

        Instantiate(impactEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
    }
}
