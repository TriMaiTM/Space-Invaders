using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private TMP_Text levelCompletedText; 
    private int nextLevelNumber;
    void Start()
    {
        Time.timeScale = 1f;
        int completedLevel = PlayerPrefs.GetInt("CompletedLevel", 1);
        if (levelCompletedText != null)
        {
            levelCompletedText.text = "Level " + completedLevel + " Completed!";
        }
        nextLevelNumber = completedLevel + 1;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + nextLevelNumber);
    }
    public void ShowLevelSelectPanel()
    {
        menu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void ShowMainMenuPanel()
    {
        menu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
   

}
