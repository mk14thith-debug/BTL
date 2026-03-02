using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public GameObject finishGame;
    public PlayerMovement playerMovement;

    public static bool isNewGame;
    private Vector3 startingPlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        finishGame.SetActive(false);
        startingPlayerPosition = playerMovement.transform.position;
        isNewGame = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Game quitted");
    }
    public void NewGame()
    {
        Time.timeScale = 1f;

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene("Level1");
    }
    public void ShowFinish()
    {
        finishGame.SetActive(true);
    }
}
