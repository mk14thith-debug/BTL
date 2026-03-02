using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    public Button continueButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("HasSavedGame", 0) == 1)
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level1");
    }

    public void Continue()
    {
        if (PlayerPrefs.GetInt("HasSavedGame", 0) == 1)
        {
            string savedScene = PlayerPrefs.GetString("SavedScene", "Level1");
            SceneManager.LoadScene(savedScene);
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}