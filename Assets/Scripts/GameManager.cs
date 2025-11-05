using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float worldSpeed;
    [Header("Level Info")]
    [SerializeField] private int currentLevel = 1;
    public int score;
    [SerializeField] private int targetScore = 100;
    private bool levelCompleted = false;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        score = 0;
        levelCompleted = false;
        HUDController.Instance.UpdateScoreSlider(score, targetScore);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (HUDController.Instance.pausePanel.activeSelf == false)
        {
            HUDController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            HUDController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            SpaceshipController.Instance.DisableBoost();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver()
    {
        StartCoroutine(GameOverScreen());
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");
    }

    public void AddScore(int amount)
    {
        if (levelCompleted) return; 

        score += amount;
        HUDController.Instance.UpdateScoreSlider(score, targetScore); 

        if (score >= targetScore)
        {
            levelCompleted = true;
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        SpaceshipController.Instance.DisableBoost();
        SpaceshipController.Instance.enabled = false;
        PlayerPrefs.SetInt("CompletedLevel", currentLevel);

        StartCoroutine(LevelCompleteScreen());
    }

    IEnumerator LevelCompleteScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level Completed"); 
    }
}