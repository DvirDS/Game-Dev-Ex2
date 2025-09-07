using UnityEngine;

public class GameManager : MonoBehaviour
{
    float elapsed;
    bool isGameOver;
    [SerializeField] float pausedTimeScale = 0f;
    [SerializeField] float normalTimeScale = 1f;

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
        Time.timeScale = pausedTimeScale;
        GameEvents.GameOver?.Invoke();
    }

    public void Restart()
    {
        isGameOver = false;
        elapsed = 0f;
        Time.timeScale = normalTimeScale;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
