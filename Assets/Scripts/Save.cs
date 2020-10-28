using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance;

    public int currentCoins, currentHealth, maxHealth, currentKevlar, maxKevlar;
    public List<string> availableGunsNames = new List<string>();
    public string levelName;
    public bool save = true;
    public bool load;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        load = false;

        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            currentCoins = data.currentCoins;
            currentHealth = data.currentHealth;
            maxHealth = data.maxHealth;
            currentKevlar = data.currentKevlar;
            maxKevlar = data.maxKevlar;
            availableGunsNames = data.availableGunsNames;
            levelName = data.levelName;
        }
        else
        {
            save = false;
        }

    }

    public void LoadSave()
    {
        if (save == true)
        {
            load = true;
        }
    }
    
}
