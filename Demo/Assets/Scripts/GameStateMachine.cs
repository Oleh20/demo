public enum GameState
{
    None,
    Playing,
    GameOver,
    Paused
}

public class GameStateMachine
{
    public GameState CurrentState { get; private set; } = GameState.None;

    public void SetState(GameState newState)
    {
        CurrentState = newState;
    }

    public bool Is(GameState state) => CurrentState == state;
}
