using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonManage : MonoBehaviour
{
    public Button firstSelectedButton;

    public AudioSource audioSource;
    public AudioClip navigateSound;
    public AudioClip selectSound;

    private GameObject lastSelected;

    void Start()
    {
        Time.timeScale = 1f; 

        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
            lastSelected = firstSelectedButton.gameObject;
        }
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != lastSelected)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                PlayNavigateSound();
                lastSelected = EventSystem.current.currentSelectedGameObject;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                PlaySelectSound();
        }
            }
    }

    void PlayNavigateSound()
    {
        if (audioSource != null && navigateSound != null)
            audioSource.PlayOneShot(navigateSound);
    }

    void PlaySelectSound()
    {
        if (audioSource != null && selectSound != null)
            audioSource.PlayOneShot(selectSound);
    }
}