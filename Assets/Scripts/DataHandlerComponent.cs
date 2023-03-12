using System;
using UnityEngine;

public class DataHandlerComponent : MonoBehaviour
{
    [SerializeField] private int remainingYears = 50;

    public int RemainingYears
    {
        get => remainingYears;
        set
        {
            remainingYears = value;
            OnRemainingYearsChanged?.Invoke(remainingYears);
        }
    }

    [SerializeField] private int maxRemainingYears = 50;
    [SerializeField] private int wave = 1;
    [SerializeField] private int woodAmount;

    public int WoodAmount
    {
        get => woodAmount;
        set
        {
            woodAmount = value;
            OnWoodAmountChanged?.Invoke(value);
        }
    }

    public int StoneAmount
    {
        get => stoneAmount;
        set
        {
            stoneAmount = value;
            OnStoneAmountChanged?.Invoke(value);
        }
    }

    [SerializeField] private int stoneAmount;

    public delegate void WoodAmountChanged(int newValue);

    public delegate void StoneAmountChanged(int newValue);

    public delegate void RemainingYearsChanged(int newValue);

    public static WoodAmountChanged OnWoodAmountChanged;
    public static StoneAmountChanged OnStoneAmountChanged;
    public static RemainingYearsChanged OnRemainingYearsChanged;

    private void Start()
    {
        TreeComponent.OnDropWood += OnDropWood;
        StoneComponent.OnStoneDrop += OnStoneDrop;
        StatueComponent.OnPrayed += OnPrayed;
        EnemyController.OnReducePlayerLifetime += OnReducePlayerLifetime;
        PassingTimeComponent.OnYearPassed += OnYearPassed;

        OnRemainingYearsChanged?.Invoke(remainingYears);
        OnWoodAmountChanged?.Invoke(woodAmount);
        OnStoneAmountChanged?.Invoke(stoneAmount);
    }

    private void OnYearPassed()
    {
        RemainingYears--;
    }

    private void OnReducePlayerLifetime(int amount)
    {
        RemainingYears -= amount;
    }

    private void OnPrayed(int amount)
    {
        RemainingYears = Math.Min(remainingYears + amount, maxRemainingYears);
    }

    private void OnStoneDrop(int amount)
    {
        StoneAmount += amount;
    }

    private void OnDropWood(int amount)
    {
        WoodAmount += amount;
    }

    private void OnDestroy()
    {
        TreeComponent.OnDropWood -= OnDropWood;
        StoneComponent.OnStoneDrop -= OnStoneDrop;
        StatueComponent.OnPrayed -= OnPrayed;
        EnemyController.OnReducePlayerLifetime -= OnReducePlayerLifetime;
    }
}