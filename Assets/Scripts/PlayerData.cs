using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int currentCoins;
    public int currentHealth;
    public int maxHealth;
    public int currentKevlar;
    public int maxKevlar;
    public List<string> availableGunsNames = new List<string>();
    public string levelName;

    public PlayerData (PlayerHealthController phc, LevelManager lm, PlayerController pc)
    {
        currentCoins = lm.currentCoins;
        currentHealth = phc.currentHealth;
        currentKevlar = phc.currentKevlar;
        maxHealth = phc.maxHealth;
        maxKevlar = phc.maxKevlar;
        levelName = lm.levelName;

        foreach (Gun gun in pc.availableGuns)
        {
            if(gun.weaponName != "Pistol")
            {
                availableGunsNames.Add(gun.weaponName);
            }
        }
        
    }
}
