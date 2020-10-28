using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startLevel;
    private string levelToLoad;

    public Texture2D cursorTexture;

    void Start()
    {
        StartCoroutine("ChangeLevelName");
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    IEnumerator ChangeLevelName()
    {
        yield return new WaitForSeconds(0.01f);
        levelToLoad = Save.instance.levelName;
        //Debug.Log(levelToLoad);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        Save.instance.LoadSave();
        SceneManager.LoadScene(levelToLoad);
    }
}
