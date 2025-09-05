using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void OnEnable()
    {
        GameEvents.TimeUpdated += OnTimeUpdated;
    }

    void OnDisable()
    {
        GameEvents.TimeUpdated -= OnTimeUpdated;
    }

    void OnTimeUpdated(float t)
    {
        if (scoreText) scoreText.text = $"Score: {(int)t}";
    }
}
