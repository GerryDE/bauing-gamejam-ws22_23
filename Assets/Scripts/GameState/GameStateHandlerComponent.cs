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
    public delegate void GameStatePauseEntered();
    public delegate void GameStateResumeEntered();
    public static GameStateChanged OnGameStateChanged;
    public static GameStatePauseEntered OnGameStatePauseEntered;
    public static GameStateResumeEntered OnGameStateResumeEntered;

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
        GameInputHandlerComponent.OnPauseCalled += OnPauseButtonPressed;
        GameInputHandlerComponent.OnResumeCalled += OnResumeButtonPressed;
        OnGameStateChanged += OnGameStateChangedFunction;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GlobalGameState = GameState.RUNNING;
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
            OnGameStatePauseEntered?.Invoke();
        }
        else if (gameState == GameState.RUNNING)
        {
            OnGameStateResumeEntered?.Invoke();
        }
    }

    private void OnDestroy()
    {
        GameInputHandlerComponent.OnPauseCalled -= OnPauseButtonPressed;
        GameInputHandlerComponent.OnResumeCalled -= OnResumeButtonPressed;
    }
}
