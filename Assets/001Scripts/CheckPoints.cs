using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndCheckpoint : MonoBehaviour
{
    public string nextSceneName;
    public Vector2 spawnPositionInNextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Lưu vào GameManager (dùng trong runtime)
        GameManager.Instance.currentLevel = nextSceneName;
        GameManager.Instance.checkpointPosition = spawnPositionInNextScene;

        // Lưu vào PlayerPrefs (dùng cho Continue sau khi tắt game)
        PlayerPrefs.SetString("SavedScene", nextSceneName);
        PlayerPrefs.SetFloat("PlayerPosX", spawnPositionInNextScene.x);
        PlayerPrefs.SetFloat("PlayerPosY", spawnPositionInNextScene.y);
        PlayerPrefs.SetInt("HasSavedGame", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(nextSceneName);
    }
}