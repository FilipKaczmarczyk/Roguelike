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

    public GameObject gameOverScreen, pauseScreen, mapDisplay, bigMapText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeInBlack, fadeOutBlack;

    public string newGameScene, mainMenuScene;

    public Image GunInUseImage;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        fadeOutBlack = true;
        fadeInBlack = false;
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

        SceneManager.LoadScene(mainMenuScene);
    }

    public void Resume()
    {
        LevelManager.instance.PauseAndUnpause();
    }
}
