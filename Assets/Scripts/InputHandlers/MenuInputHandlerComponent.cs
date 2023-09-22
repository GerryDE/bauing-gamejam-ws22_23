using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MenuInputHandlerComponent : MonoBehaviour
{
    private PlayerInput _playerInput;

    public delegate void ScrollbarButtonTriggered(float value);
    public delegate void ExitButtonTriggered();

    public static ScrollbarButtonTriggered OnScrollbarButtonTriggered;
    public static ExitButtonTriggered OnExitButtonTriggered;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void OnScollbar(InputValue value)
    {
        OnScrollbarButtonTriggered?.Invoke(value.Get<float>());
    }

    public void OnExit(InputValue value)
    {
        OnExitButtonTriggered?.Invoke();
    }
}