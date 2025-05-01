using UnityEngine;

public static class ScoreStorage
{
    const string BestScoreKey = "BestScore";

    public static int GetBestScore() => PlayerPrefs.GetInt(BestScoreKey, 0);
    public static void SaveBestScore(int score) => PlayerPrefs.SetInt(BestScoreKey, score);
}
