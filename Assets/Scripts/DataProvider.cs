using Data;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    // Make sure that this component only exists once in the project to the keep the Singleton approach
    public static DataProvider Instance { get; private set; }

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
    }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private ResourceData resourceData;

    public PlayerData PlayerData => playerData;
    public ResourceData ResourceData => resourceData;
}
