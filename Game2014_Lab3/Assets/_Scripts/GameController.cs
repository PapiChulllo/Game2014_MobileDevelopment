using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;

    private int score = 0;

    public void ChangeScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        _scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene
    }

    public void GameOver()
    {
        // Game over logic (e.g., display game over screen, stop player movement, etc.)
        Debug.Log("Game Over!");
        // You can call RestartGame() here or show a "Game Over" screen
    }
}
