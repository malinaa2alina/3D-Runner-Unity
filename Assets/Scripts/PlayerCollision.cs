using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel; // Панель "Game Over"

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // Столкновение с препятствием
        {
            Time.timeScale = 0f; // Остановить игру
            gameOverPanel.SetActive(true); // Включить панель
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    // Кнопка "Выход"
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
