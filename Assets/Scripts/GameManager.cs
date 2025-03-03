using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    private int score; 
    public int lives; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    public TextMeshProUGUI gameOverText;
    public bool isGameActive;

    public Button restartButton;
    public Button pauseButton;
    public Button continueButton;
    public GameObject titleScreen;

    public GameObject pauseScreen;
    public bool paused;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        lives = 5;

        StartCoroutine(SpawnTarget());
        Updatescore(0);
        livesText.text = "Lives:" + lives;
        titleScreen.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive) { 
            yield return new WaitForSeconds(spawnRate); 
            int index = Random.Range(0, targets.Count); 
            Instantiate(targets[index]);
        }
    }
    public void Updatescore(int scoreToAdd) 
    { 
        score += scoreToAdd; 
        scoreText.text = "Score:" + score;
    }
    public void UpdateLives()
    {
        if (isGameActive)
        {
            lives -= 1;
            livesText.text = "Lives:" + lives;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P) && isGameActive)
        //{
        //    ChangePaused();
        //}
    }

    public void ChangePaused()
    {
        if (isGameActive)
        {
            if (!paused)
            {
                paused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }


    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
