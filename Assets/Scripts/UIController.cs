using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    float lastScore;

    void OnEnable()
    {
        GameEvents.TimeUpdated += OnTimeUpdated;
        GameEvents.GameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameEvents.TimeUpdated -= OnTimeUpdated;
        GameEvents.GameOver -= OnGameOver;
    }

    void OnTimeUpdated(float t)
    {
        lastScore = t;
        if (scoreText) scoreText.text = $"Score: {(int)t}";
    }

    void OnGameOver()
    {
        if (scoreText) scoreText.gameObject.SetActive(false);
        if (finalScoreText) finalScoreText.text = $"Score: {(int)lastScore}";
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }
}
