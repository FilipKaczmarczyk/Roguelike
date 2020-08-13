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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
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
}
