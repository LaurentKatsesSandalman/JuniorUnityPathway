using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private GameObject mainCamera;
    public Slider soundSlider;
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
        float currentVolume = GameObject.Find("Main Camera").GetComponent<AudioSource>().volume;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        startScreen.gameObject.SetActive(true);
        soundSlider = GameObject.Find("VolSlider").GetComponent<Slider>();
        Debug.Log("Current Volume: " + currentVolume);
        Debug.Log("Sound Slider Volume: " + soundSlider.value);
        soundSlider.SetValueWithoutNotify(currentVolume);
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
