using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManagement : MonoBehaviour
{
    public AudioClip openingTheme;
    public AudioClip menuIntro;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = openingTheme;
            audioSource.Play();
            StartCoroutine(PlayMenuIntroAfterOpening());
        }
    }

    void Update()
    {
        
    }

    private System.Collections.IEnumerator PlayMenuIntroAfterOpening()
    {
        yield return new WaitForSeconds(openingTheme.length);

        audioSource.clip = menuIntro;
        audioSource.loop = true;
        audioSource.Play();
    }
}
