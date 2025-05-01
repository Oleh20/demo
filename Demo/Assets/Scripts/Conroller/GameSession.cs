public class GameSession
{
    private readonly GameModel _model;
    private readonly GameSettings _settings;
    private readonly TargetSpawner _spawner;
    private readonly UIManager _uiManager;

    private int _lastLevel;

    public GameSession(GameSettings settings, TargetSpawner spawner, UIManager uiManager)
    {
        _model = new GameModel(settings.maxLives);
        _settings = settings;
        _spawner = spawner;
        _uiManager = uiManager;

        _spawner.OnTargetClicked += HandleTargetClick;

        _uiManager.UpdateScore(_model.Score);
        _uiManager.UpdateLives(_model.Lives);
    }

    public bool IsGameOver => _model.IsGameOver();

    public int Score => _model.Score;

    public void Stop()
    {
        _spawner.StopSpawn();
        _spawner.enabled = false;
    }

    private void HandleTargetClick(TargetModel targetModel)
    {
        int delta = targetModel.Type == TargetType.Good ? 1 : -1;
        _model.AddScore(delta);

        if (delta < 0)
            _model.LoseLife();

        _uiManager.UpdateScore(_model.Score);
        _uiManager.UpdateLives(_model.Lives);

        if (_model.IsGameOver())
            return;

        UpdateDifficulty();
    }

    private void UpdateDifficulty()
    {
        int level = _model.Score / _settings.scorePerLevel;
        if (level > _lastLevel)
        {
            _lastLevel = level;
            _spawner.DecreaseSpawnInterval(_settings.spawnDecreasePerLevel);
        }
    }
}
