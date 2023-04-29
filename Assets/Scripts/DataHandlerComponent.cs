using System;
using Data;
using UnityEngine;

public class DataHandlerComponent : MonoBehaviour
{
    [SerializeField] private int waveCount = 1;
    [SerializeField] private int woodAmount;
    [SerializeField] private int currentFenceVersion;
    [SerializeField] private int currentStatueVersion;
    [SerializeField] private int currentMineVersion;

    private static PlayerData _playerData;

    public int CurrentMineVersion
    {
        get => currentMineVersion;
        set
        {
            currentMineVersion = value;
            OnMineVersionChanged?.Invoke(currentMineVersion);
        }
    }

    [SerializeField] private int currentTreeVersion;

    public int CurrentTreeVersion
    {
        get => currentTreeVersion;
        set
        {
            currentTreeVersion = value;
            OnTreeVersionChanged?.Invoke(currentTreeVersion);
        }
    }

    [SerializeField] AfterEffects postProcessingCameraScript;
    [SerializeField] UIController uiScript;

    public int Wave
    {
        get => waveCount;
        set
        {
            waveCount = value;
            OnWaveCountChanged.Invoke(waveCount);
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

    [SerializeField] private AudioClip attackAudioClip;
    [SerializeField] private AudioClip attackPlayerAudioClip;
    [SerializeField] private AudioClip miningAudioClip;
    [SerializeField] private AudioClip praisingAudioClip;
    [SerializeField] private AudioClip woodCuttingAudioClip;
    [SerializeField] private AudioClip upgradingAudioClip;

    public void PlayAttackAudioClip()
    {
        audioSource.clip = attackAudioClip;
        audioSource.PlayOneShot(attackAudioClip);
    }

    public void PlayAttackPlayerAudioClip()
    {
        audioSource.clip = attackPlayerAudioClip;
        audioSource.PlayOneShot(attackPlayerAudioClip);
    }

    public void PlayMiningAudioClip()
    {
        audioSource.clip = miningAudioClip;
        audioSource.PlayOneShot(miningAudioClip);
    }

    public void PlayPraisingAudioClip()
    {
        audioSource.clip = praisingAudioClip;
        audioSource.PlayOneShot(praisingAudioClip);
    }

    public void PlayWoodCuttingAudioClip()
    {
        audioSource.clip = woodCuttingAudioClip;
        audioSource.PlayOneShot(woodCuttingAudioClip);
    }

    public void PlayUpgradingAudioClip()
    {
        audioSource.clip = upgradingAudioClip;
        audioSource.PlayOneShot(upgradingAudioClip);
    }

    public void PlayerAttackAudioClip()
    {
        audioSource.PlayOneShot(attackAudioClip);
    }

    /// <summary>
    /// 1 Attacking
    /// 2 Mining
    /// 3 Praising
    /// 4 Upgrading
    /// 5 Woodcutting
    /// </summary>
    public delegate void WoodAmountChanged(int newValue);

    public delegate void StoneAmountChanged(int newValue);

    public delegate void WaveCountChanged(int newValue);

    public delegate void StatueVersionChanged(int newValue);

    public delegate void MineVersionChanged(int newValue);

    public delegate void TreeVersionChanged(int newVersion);

    public static WoodAmountChanged OnWoodAmountChanged;
    public static StoneAmountChanged OnStoneAmountChanged;
    public static WaveCountChanged OnWaveCountChanged;
    public static StatueVersionChanged OnStatueVersionChanged;
    public static MineVersionChanged OnMineVersionChanged;
    public static TreeVersionChanged OnTreeVersionChanged;
    private AudioSource audioSource;

    private void Update()
    {
    }

    private void Start()
    {
        _playerData = DataProvider.Instance.PlayerData;
            
        TreeComponent.OnDropWood += OnDropWood;
        StoneComponent.OnStoneDrop += OnStoneDrop;
        StatueComponent.OnPrayed += OnPrayed;
        EnemyController.OnReducePlayerLifetime += OnReducePlayerLifetime;
        PassingTimeComponent.OnYearPassed += OnYearPassed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;
        StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        StoneUpgradeComponent.OnUpgradeMine += OnUpgradeMine;
        TreeUpgradeComponent.OnUpgradeTree += OnUpgradeTree;

        OnWoodAmountChanged?.Invoke(woodAmount);
        OnStoneAmountChanged?.Invoke(stoneAmount);
        OnWaveCountChanged?.Invoke(waveCount);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnUpgradeTree()
    {
        CurrentTreeVersion++;
        PlayUpgradingAudioClip();
    }

    private void OnUpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite)
    {
        CurrentMineVersion++;
        PlayUpgradingAudioClip();
    }

    private void OnUpgradeStatue(int newAgeValue, Sprite sprite)
    {
        _playerData.MaxRemainingYears = newAgeValue;
        PlayUpgradingAudioClip();
    }

    private void OnBossDestroyed()
    {
        Wave++;
    }

    private void OnYearPassed()
    {
        _playerData.CurrentRemainingYears--;
    }

    private void OnReducePlayerLifetime(int amount)
    {
        _playerData.CurrentRemainingYears -= amount;
    }

    private void OnPrayed(int amount)
    {
        _playerData.CurrentRemainingYears =
            Math.Min(_playerData.CurrentRemainingYears + amount, _playerData.MaxRemainingYears);
    }

    private void OnStoneDrop(int amount)
    {
        StoneAmount += amount;
        uiScript.giveFeedbackWithValues(amount, "Stone");
    }

    private void OnDropWood(int amount)
    {
        WoodAmount += amount;
        uiScript.giveFeedbackWithValues(amount, "Wood");
    }

    private void OnDestroy()
    {
        TreeComponent.OnDropWood -= OnDropWood;
        StoneComponent.OnStoneDrop -= OnStoneDrop;
        StatueComponent.OnPrayed -= OnPrayed;
        EnemyController.OnReducePlayerLifetime -= OnReducePlayerLifetime;
        PassingTimeComponent.OnYearPassed -= OnYearPassed;
        BossComponent.OnBossDestroyed -= OnBossDestroyed;
        StatueUpgradeComponent.OnUpgradeStatue -= OnUpgradeStatue;
        StoneUpgradeComponent.OnUpgradeMine -= OnUpgradeMine;
        TreeUpgradeComponent.OnUpgradeTree -= OnUpgradeTree;
    }
}