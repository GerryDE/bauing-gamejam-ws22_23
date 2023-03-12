using System;
using UnityEngine;

public class DataHandlerComponent : MonoBehaviour
{
    private int wave;
    private int woodAmount;
    private int stoneAmount;
    
    public delegate void WoodAmountChanged(int newValue);

    public static WoodAmountChanged OnWoodAmountChanged;

    private void Start()
    {
        TreeComponent.OnDropWood += OnDropWood;
    }

    private void OnDropWood(int amount)
    {
        woodAmount += amount;
        OnWoodAmountChanged?.Invoke(woodAmount);
    }
}
