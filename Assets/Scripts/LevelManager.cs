using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 3f;

    public string levelName;

    public string nextLevel;

    public bool isPause = false;

    public int currentCoins;

    public Transform startPoint;

    public GameObject tracker;

    public GameObject player;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameObject Player = Instantiate(player, startPoint.position, Quaternion.identity);
        }

        levelName = SceneManager.GetActiveScene().name; 

        PlayerController.instance.transform.position = startPoint.position;
        PlayerController.instance.canMove = true;

        if (Save.instance.load == true)
        {
            currentCoins = Save.instance.currentCoins;
        }
        else
        {
            currentCoins = Tracker.instance.currentCoins;
        }

        Time.timeScale = 1f;

        UIController.instance.goldText.text = currentCoins.ToString();
        UIController.instance.levelText.text = levelName;
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
        Save.instance.load = false;

        AudioManager.instance.PlaySFX(6);

        AudioManager.instance.StopLevelMusic();

        UIController.instance.StartFadeInBlack();

        PlayerController.instance.canMove = false;

        yield return new WaitForSeconds(waitToLoad);

        if (typeof(Tracker) != null)
        {
            Debug.Log("Traker nie istnieje");
            Instantiate(tracker, Vector3.zero, Quaternion.identity);
        }

        Tracker.instance.currentCoins = currentCoins;
        Tracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        Tracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
        Tracker.instance.currentKevlar = PlayerHealthController.instance.currentKevlar;
        Tracker.instance.maxKevlar = PlayerHealthController.instance.maxKevlar;

        SceneManager.LoadScene(nextLevel);

        if (nextLevel == "Victory")
        {
            PlayerController.instance.gameObject.SetActive(false);
        }
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
