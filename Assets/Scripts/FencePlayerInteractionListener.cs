using UnityEngine;

public class FencePlayerInteractionListener : MonoBehaviour
{
    public delegate void OnHealFence();

    public static OnHealFence onHealFence;

    void Awake()
    {
        PlayerController.onPlayerFenceInteraction += PlayerFenceInteractionTriggered;
    }

    void PlayerFenceInteractionTriggered()
    {
        onHealFence?.Invoke();
    }
}