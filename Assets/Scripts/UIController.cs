using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthBar;
    public Text healthText;

    public Slider kevlarBar;
    public Text kevlarText;

    public Text goldText;

    public Text levelText;

    public GameObject gameOverScreen, pauseScreen, mapDisplay, bigMapText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeInBlack, fadeOutBlack;

    public string newGameScene, mainMenuScene;

    public Image GunInUseImage;

    public Slider bossHealthBar;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        fadeOutBlack = true;
        fadeInBlack = false;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GunInUseImage.sprite = PlayerController.instance.availableGuns[PlayerController.instance.gunInUse].gunImage;
        }
        
    }

    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeInBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeInBlack = false;
            }
        }
    }

    public void StartFadeInBlack()
    {
        fadeOutBlack = false;
        fadeInBlack = true;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGameScene);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        PlayerController.instance.gameObject.SetActive(false);

        SceneManager.LoadScene(mainMenuScene);
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(PlayerHealthController.instance, LevelManager.instance, PlayerController.instance);
    }

    public void Resume()
    {
        LevelManager.instance.PauseAndUnpause();
    }
}
