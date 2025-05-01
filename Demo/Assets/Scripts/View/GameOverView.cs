using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IGameOverView
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _finalScoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    public void Show(int finalScore)
    {
        _panel.SetActive(true);
        _finalScoreText.text = $"Score: {finalScore}";

        int best = ScoreStorage.GetBestScore();
        if (finalScore > best)
        {
            best = finalScore;
            ScoreStorage.SaveBestScore(best);
        }

        _bestScoreText.text = $"Best: {best}";

        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(() => RestartGame());

        _exitButton.onClick.RemoveAllListeners();
        _exitButton.onClick.AddListener(() => Exit());
    }
    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    private void Exit()
    {
        Application.Quit();
    }

    public void Hide()
    {
        _panel.SetActive(false);
    }
}
