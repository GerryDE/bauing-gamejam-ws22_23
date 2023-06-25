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
    public delegate void GameStatePause();
    public delegate void GameStateResume();
    public static GameStateChanged OnGameStateChanged;
    public static GameStatePause OnGameStatePause;
    public static GameStateResume OnGameStateResume;

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
        GameInputHandlerComponent.OnResumeButtonPressed += OnResumeButtonPressed;
        OnGameStateChanged += OnGameStateChangedFunction;
    }

    private void OnPauseButtonPressed()
    {
        GlobalGameState = GameState.PAUSED;
    }

    private void OnResumeButtonPressed()
    {
        GlobalGameState = GameState.RUNNING;
    }

    private void OnGameStateChangedFunction(GameState gameState)
    {
        if (gameState == GameState.PAUSED)
        {
            OnGameStatePause?.Invoke();
        }
        else if (gameState == GameState.RUNNING)
        {
            OnGameStateResume?.Invoke();
        }
    }

    private void OnDestroy()
    {
        GameInputHandlerComponent.OnPauseButtonPressed -= OnPauseButtonPressed;
        GameInputHandlerComponent.OnResumeButtonPressed -= OnResumeButtonPressed;
    }
}
