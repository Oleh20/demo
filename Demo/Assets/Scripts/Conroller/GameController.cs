using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private TargetSpawner _spawner;
    [SerializeField] private UIManager _uiManager;

    private GameSession _session;
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new GameStateMachine();
        _stateMachine.SetState(GameState.Playing);

        _session = new GameSession(_gameSettings, _spawner, _uiManager);
        _spawner.OnTargetClicked += OnTargetClickCheckGameOver;
    }

    private void OnTargetClickCheckGameOver(TargetModel _)
    {
        if (_stateMachine.Is(GameState.Playing) && _session.IsGameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _session.Stop();
        _stateMachine.SetState(GameState.GameOver);
        _uiManager.ShowGameOver(_session.Score);
    }
}
