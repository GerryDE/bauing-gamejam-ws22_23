using System;
using UnityEngine;

public class DataHandlerComponent : MonoBehaviour
{
    [SerializeField] private int remainingYears = 50;
    [SerializeField] private int maxRemainingYears = 50;
    [SerializeField] private int wave = 1;
    [SerializeField] private int woodAmount;
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
        remainingYears--;
        OnRemainingYearsChanged?.Invoke(remainingYears);
    }

    private void OnReducePlayerLifetime(int amount)
    {
        remainingYears -= amount;
        OnRemainingYearsChanged?.Invoke(remainingYears);
    }

    private void OnPrayed(int amount)
    {
        remainingYears = Math.Min(remainingYears + amount, maxRemainingYears);
        OnRemainingYearsChanged?.Invoke(remainingYears);
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

    private void OnDestroy()
    {
        TreeComponent.OnDropWood -= OnDropWood;
        StoneComponent.OnStoneDrop -= OnStoneDrop;
        StatueComponent.OnPrayed -= OnPrayed;
        EnemyController.OnReducePlayerLifetime -= OnReducePlayerLifetime;
    }
}