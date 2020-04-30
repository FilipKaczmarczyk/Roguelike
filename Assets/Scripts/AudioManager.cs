using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic, gameOverMusic;

    public AudioSource[] sfx;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        levelMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();

        gameOverMusic.Play();
    }

    public void PlaySFX(int sfxNumber)
    {
        sfx[sfxNumber].Stop();
        sfx[sfxNumber].Play();
    }
}
