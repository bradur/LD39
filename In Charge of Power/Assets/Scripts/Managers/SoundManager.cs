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

    private bool isOn = true;

    void Awake()
    {
        main = this;
    }

    public void PlaySound(SoundType soundType)
    {
        if (isOn)
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

    public bool Toggle()
    {
        isOn = !isOn;
        return isOn;
    }
}

[System.Serializable]
public class GameSound : System.Object
{

    public SoundType soundType;
    public AudioSource sound;

}
