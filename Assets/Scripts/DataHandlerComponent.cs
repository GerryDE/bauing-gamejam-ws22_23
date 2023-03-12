using System;
using UnityEngine;

public class DataHandlerComponent : MonoBehaviour
{
    private int wave;
    private int woodAmount;
    private int stoneAmount;
    
    public delegate void WoodAmountChanged(int newValue);
    public delegate void StoneAmountChanged(int newValue);

    public static WoodAmountChanged OnWoodAmountChanged;
    public static StoneAmountChanged OnStoneAmountChanged;

    private void Start()
    {
        TreeComponent.OnDropWood += OnDropWood;
        StoneComponent.OnStoneDrop += OnStoneDrop;
    }

    private void OnStoneDrop(int amount)
    {
        stoneAmount += amount;
        OnStoneAmountChanged?.Invoke(stoneAmount);
    }

    private void OnDropWood(int amount)
    {
        woodAmount += amount;
        OnWoodAmountChanged?.Invoke(woodAmount);
    }
}
