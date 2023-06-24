using UnityEngine;

public class GameStateHandlerComponent : MonoBehaviour
{

    public enum GameState
    {
        RUNNING,
        PAUSED
    }

    // Make sure that this component only exists once in the project to the keep the Singleton approach
    public static GameStateHandlerComponent Instance { get; private set; }

    public delegate void GameStateChanged(GameState gameState);
    public static GameStateChanged OnGameStateChanged;

    private GameState _globalGameState;

    public GameState GlobalGameState
    {
        get => _globalGameState;
        set 
        {
            _globalGameState = value;
            OnGameStateChanged?.Invoke(value);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GlobalGameState = GameState.RUNNING;

        GameInputHandlerComponent.OnPauseButtonPressed += OnPauseButtonPressed;
    }

    private void OnPauseButtonPressed()
    {
        if (GlobalGameState == GameState.PAUSED)
        {
            GlobalGameState = GameState.RUNNING;
        }
        else if (GlobalGameState == GameState.RUNNING)
        {
            GlobalGameState = GameState.PAUSED;
        }
        else
        {
            Debug.LogError("ERROR: Unknown gameState: " + _globalGameState);
        }
    }

    private void OnDestroy()
    {
        GameInputHandlerComponent.OnPauseButtonPressed -= OnPauseButtonPressed;
    }
}
