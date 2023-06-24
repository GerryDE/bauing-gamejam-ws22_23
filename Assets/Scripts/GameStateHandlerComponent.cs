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

    private GameState _gameState;

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

        _gameState = GameState.RUNNING;

        GameInputHandlerComponent.OnPauseButtonPressed += OnPause;
    }

    private void OnPause()
    {
        if (_gameState == GameState.PAUSED)
        {
            _gameState = GameState.RUNNING;
        }
        else if (_gameState == GameState.RUNNING)
        {
            _gameState = GameState.PAUSED;
        }
        else
        {
            Debug.LogError("ERROR: Unknown gameState: " + _gameState);
        }
    }

    public GameState GetGameState()
    {
        return _gameState;
    }

    private void OnDestroy()
    {
        GameInputHandlerComponent.OnPauseButtonPressed -= OnPause;
    }
}
