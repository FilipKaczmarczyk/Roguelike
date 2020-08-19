using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 3f;

    public string nextLevel;

    public bool isPause = false;

    public int currentCoins;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        UIController.instance.goldText.text = currentCoins.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAndUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlaySFX(6);

        AudioManager.instance.StopLevelMusic();

        UIController.instance.StartFadeInBlack();

        PlayerController.instance.canMove = false;

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseAndUnpause()
    {
        if (!isPause)
        {
            UIController.instance.pauseScreen.SetActive(true);
            isPause = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseScreen.SetActive(false);
            isPause = false;
            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;

        UIController.instance.goldText.text = currentCoins.ToString();
    }
    public void SpendCoins(int amount)
    {
        currentCoins -= amount;

        if(currentCoins < 0)
        {
            currentCoins = 0;
        }

        UIController.instance.goldText.text = currentCoins.ToString();
    }
}
