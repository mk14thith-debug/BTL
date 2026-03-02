using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPause;

    public PlayerMovement playerMovement;

    public float defaultMusicVolume = 1f;
    private float musicVolume;

    void Start()
    {
        pauseMenu.SetActive(false);

        musicVolume = PlayerPrefs.GetFloat("musicVolume", defaultMusicVolume);
        AudioListener.volume = musicVolume;
    }

    void Update()
    {
        // Không cần check isGrounded nữa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;

        PlayerPrefs.SetFloat("musicVolume", AudioListener.volume);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;

        musicVolume = PlayerPrefs.GetFloat("musicVolume", defaultMusicVolume);
        AudioListener.volume = musicVolume;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        isPause = false;

        PlayerPrefs.DeleteAll();   // 🔥 reset toàn bộ save
        SceneManager.LoadScene("Level1");
    }

    public void QuitAndSave()
    {
        Time.timeScale = 1f;
        isPause = false;

        Vector3 playerPosition = playerMovement.transform.position;

        // 🔥 Lưu vị trí
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);

        // 🔥 Lưu scene hiện tại
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt("HasSavedGame", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MenuScene");
    }
}