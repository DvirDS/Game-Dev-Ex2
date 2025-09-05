using UnityEngine;

public class GameManager : MonoBehaviour
{
    float elapsed;
    bool isGameOver;

    void OnEnable()
    {
        GameEvents.PlayerHit += OnPlayerHit;
    }

    void OnDisable()
    {
        GameEvents.PlayerHit -= OnPlayerHit;
    }

    void Update()
    {
        if (isGameOver) return;
        elapsed += Time.deltaTime;
        GameEvents.TimeUpdated?.Invoke(elapsed);
    }

    void OnPlayerHit()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;
        GameEvents.GameOver?.Invoke();
    }

    public void Restart()
    {
        isGameOver = false;
        elapsed = 0f;
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
