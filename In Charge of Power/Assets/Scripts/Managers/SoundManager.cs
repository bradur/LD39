// Date   : 29.07.2017 17:25
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    None,
    Click,
    NotEnoughMoney,
    CantPlaceThere
}

public class SoundManager : MonoBehaviour {

    public static SoundManager main;

    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();

    private bool sfxMuted = false;

    [SerializeField]
    private bool musicMuted = false;

    [SerializeField]
    private AudioSource musicSource;

    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        if (musicMuted)
        {
            musicSource.Pause();
            UIManager.main.ToggleMusic();
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.sound.Play();
                }
            }
        }
    }

    public void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
        UIManager.main.ToggleSfx();
    }

    public void ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
        UIManager.main.ToggleMusic();
    }
}

[System.Serializable]
public class GameSound : System.Object
{

    public SoundType soundType;
    public AudioSource sound;

}
