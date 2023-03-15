using System;
using UnityEngine;

public class DataHandlerComponent : MonoBehaviour
{
    [SerializeField] private int remainingYears = 50;
    [SerializeField] private int maxRemainingYears = 50;

    public int MaxRemainingYears
    {
        get => maxRemainingYears;
        set
        {
            maxRemainingYears = value;
            OnMaxRemainingYearsChanged?.Invoke(maxRemainingYears);
        }
    }

    [SerializeField] private int waveCount = 1;
    [SerializeField] private int woodAmount;
    [SerializeField] private int currentFenceVersion;
    [SerializeField] private int currentStatueVersion;
    [SerializeField] private int currentMineVersion;

    public int CurrentMineVersion
    {
        get => currentMineVersion;
        set
        {
            currentMineVersion = value; 
            OnMineVersionChanged?.Invoke(currentMineVersion);
        }
    }

    [SerializeField] AfterEffects postProcessingCameraScript;

    public int Wave
    {
        get => waveCount;
        set
        {
            waveCount = value;
            OnWaveCountChanged.Invoke(waveCount);
        }
    }

    public int RemainingYears
    {
        get => remainingYears;
        set
        {
            remainingYears = value;
            OnRemainingYearsChanged?.Invoke(remainingYears);
            postProcessingCameraScript.UpdateSaturaion(remainingYears * 1f);
            postProcessingCameraScript.UpdateVignette(remainingYears);
        }
    }

    public int CurrentFenceVersion
    {
        get => currentFenceVersion;
        set => currentFenceVersion = value;
    }

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

    public int CurrentStatueVersion
    {
        get => currentStatueVersion;
        set
        {
            currentStatueVersion = value;
            OnStatueVersionChanged?.Invoke(value);
        }
    }

    [SerializeField] private int stoneAmount;

    public delegate void WoodAmountChanged(int newValue);

    public delegate void StoneAmountChanged(int newValue);

    public delegate void RemainingYearsChanged(int newValue);

    public delegate void MaxRemainingYearsChanged(int newValue);

    public delegate void WaveCountChanged(int newValue);

    public delegate void StatueVersionChanged(int newValue);

    public delegate void MineVersionChanged(int newValue);

    public static WoodAmountChanged OnWoodAmountChanged;
    public static StoneAmountChanged OnStoneAmountChanged;
    public static RemainingYearsChanged OnRemainingYearsChanged;
    public static MaxRemainingYearsChanged OnMaxRemainingYearsChanged;
    public static WaveCountChanged OnWaveCountChanged;
    public static StatueVersionChanged OnStatueVersionChanged;
    public static MineVersionChanged OnMineVersionChanged;

    private void Start()
    {
        TreeComponent.OnDropWood += OnDropWood;
        StoneComponent.OnStoneDrop += OnStoneDrop;
        StatueComponent.OnPrayed += OnPrayed;
        EnemyController.OnReducePlayerLifetime += OnReducePlayerLifetime;
        PassingTimeComponent.OnYearPassed += OnYearPassed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;
        StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        StoneUpgradeComponent.OnUpgradeMine += OnUpgradeMine;

        OnRemainingYearsChanged?.Invoke(remainingYears);
        OnWoodAmountChanged?.Invoke(woodAmount);
        OnStoneAmountChanged?.Invoke(stoneAmount);
        OnWaveCountChanged?.Invoke(waveCount);
    }

    private void OnUpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite)
    {
        CurrentMineVersion++;
    }

    private void OnUpgradeStatue(int newAgeValue, Sprite sprite)
    {
        maxRemainingYears = newAgeValue;
    }

    private void OnBossDestroyed()
    {
        Wave++;
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
        PassingTimeComponent.OnYearPassed -= OnYearPassed;
        BossComponent.OnBossDestroyed -= OnBossDestroyed;
    }
}