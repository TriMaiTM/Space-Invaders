using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    public GameObject pausePanel;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider scoreSlider;
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

    public void UpdateEnergySlider(float current, float max)
    {
        energySlider.value = Mathf.RoundToInt(current);
        energySlider.maxValue = max;
        energyText.text = energySlider.value + "/" + energySlider.maxValue;
    }
    public void UpdateHealthSlider(float current, float max)
    {   
        healthSlider.maxValue = max;
        healthSlider.value = current;
        healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
    }

    public void UpdateScoreSlider(int currentScore, int maxScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "" + currentScore;
        }
        if (scoreSlider != null)
        {
            scoreSlider.maxValue = maxScore;
            scoreSlider.value = currentScore;
        }
    }
}
