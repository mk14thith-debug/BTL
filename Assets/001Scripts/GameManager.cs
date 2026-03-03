using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string currentLevel;
    public Vector3 checkpointPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string levelName)
    {
        currentLevel = levelName;
        SceneManager.LoadScene(levelName);
    }

    public void SetCheckpoint(Vector3 pos)
    {
        checkpointPosition = pos;
    }
}