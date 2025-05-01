using UnityEngine;

public class GameModel
{
    private int _score;
    private int _lives;
    public int Score => _score;
    public int Lives => _lives;

    public GameModel(int maxLives = 3)
    {
        _lives = maxLives;
    }

    public void AddScore(int amount)
    {
        _score = Mathf.Max(0, _score + amount);
    }

    public void LoseLife()
    {
        if (_lives > 0)
        {
            _lives--;
        }
    }

    public bool IsGameOver()
    {
        return _lives <= 0;
    }
}
