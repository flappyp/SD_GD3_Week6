using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] bugPrefab;
    public int bugIndex;
    public float spawnRate = 1.0f;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    private bool isGameOver;  // Variable to track game over state
    public int lives = 3;

    void Start()
    {
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        UpdateLivesText();
        Time.timeScale = 1.0f;
        isGameOver = false; // Set game over to false at the start
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;  // Pause the game
        isGameOver = true;   // Set game over to true
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTarget()
    {
        // Keep spawning bugs until game is over
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int bugIndex = Random.Range(0, bugPrefab.Length);
            Instantiate(bugPrefab[bugIndex]);
        }
    }

    public void DecreaseLives()
    {
        // Lives should only decrease if the game is NOT over
        if (!isGameOver)
        {
            lives--;
            UpdateLivesText();

            // If lives run out, trigger game over
            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
