using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public bool gameOver = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverScreen;
    public GameObject startScreen;
    private int score;
    private int lives = 3;
    private bool isGameActive=false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverScreen.gameObject.SetActive(true);
            isGameActive = false;
        }
    }

    IEnumerator SpawnTarget()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToSubtract)
    {
        lives -= livesToSubtract;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            gameOver = true;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        startScreen.gameObject.SetActive(true);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        UpdateScore(0);
        UpdateLives(0);
        StartCoroutine(SpawnTarget());
        startScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
    }

}
