using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessTrigger : MonoBehaviour
{
    public GameObject canvasFinishGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasFinishGame.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
