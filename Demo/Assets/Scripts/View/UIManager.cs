using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _livesText;
    [SerializeField] private GameOverView _gameOverView;

    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    public void UpdateLives(int lives)
    {
        _livesText.text = $"Lives: {lives}";
    }

    public void ShowGameOver(int finalScore)
    {
        _gameOverView.Show(finalScore);
    }
}
