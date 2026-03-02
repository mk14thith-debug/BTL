using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumneSlider;
    public float defaultMusicVolume = 1f;
    void Start()
    {
        

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", defaultMusicVolume);
        }
        Load();
        AudioListener.volume = volumneSlider.value;
    }

    public void ChangeVolume()
    {
       AudioListener.volume = volumneSlider.value;
        Save();
    }
    public void Load()
    {
        volumneSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumneSlider.value);
    }
}
