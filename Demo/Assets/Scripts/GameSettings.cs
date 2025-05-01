using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Game Rules")]
    public int maxLives = 3;
    public float spawnInterval = 1.5f;

    [Header("Impulse")]
    public float impulseMin = 4f;
    public float impulseMax = 6f;

    [Header("Difficulty")]
    public int scorePerLevel = 5;
    public float spawnDecreasePerLevel = 0.1f;

    [Header("Target Lifetime")]
    public float targetLifetime = 3f;
}
