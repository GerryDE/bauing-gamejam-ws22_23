using System;
using UnityEngine;

public class DataHandlerComponent : MonoBehaviour
{
    [SerializeField] private int waveCount = 1;
    [SerializeField] private int currentFenceVersion;
    [SerializeField] private int currentStatueVersion;
    [SerializeField] private int currentMineVersion;

    private DataProvider.CurrentPlayerData _currentPlayerData;
    private DataProvider.CurrentResourceData _resourceData;

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

    public int CurrentStatueVersion
    {
        get => currentStatueVersion;
        set
        {
            currentStatueVersion = value;
            OnStatueVersionChanged?.Invoke(value);
        }
    }

    [SerializeField] private AudioClip attackAudioClip;
    [SerializeField] private AudioClip attackPlayerAudioClip;
    [SerializeField] private AudioClip miningAudioClip;
    [SerializeField] private AudioClip praisingAudioClip;
    [SerializeField] private AudioClip woodCuttingAudioClip;
    [SerializeField] private AudioClip upgradingAudioClip;

    public void PlayAttackAudioClip()
    {
        _audioSource.clip = attackAudioClip;
        _audioSource.PlayOneShot(attackAudioClip);
    }

    public void PlayAttackPlayerAudioClip()
    {
        _audioSource.clip = attackPlayerAudioClip;
        _audioSource.PlayOneShot(attackPlayerAudioClip);
    }

    public void PlayMiningAudioClip()
    {
        _audioSource.clip = miningAudioClip;
        _audioSource.PlayOneShot(miningAudioClip);
    }

    public void PlayPraisingAudioClip()
    {
        _audioSource.clip = praisingAudioClip;
        _audioSource.PlayOneShot(praisingAudioClip);
    }

    public void PlayWoodCuttingAudioClip()
    {
        _audioSource.clip = woodCuttingAudioClip;
        _audioSource.PlayOneShot(woodCuttingAudioClip);
    }

    public void PlayUpgradingAudioClip()
    {
        _audioSource.clip = upgradingAudioClip;
        _audioSource.PlayOneShot(upgradingAudioClip);
    }

    public void PlayerAttackAudioClip()
    {
        _audioSource.PlayOneShot(attackAudioClip);
    }

    /// <summary>
    /// 1 Attacking
    /// 2 Mining
    /// 3 Praising
    /// 4 Upgrading
    /// 5 Woodcutting
    /// </summary>
    public delegate void WaveCountChanged(int newValue);

    public delegate void StatueVersionChanged(int newValue);

    public delegate void MineVersionChanged(int newValue);

    public delegate void TreeVersionChanged(int newVersion);

    public static WaveCountChanged OnWaveCountChanged;
    public static StatueVersionChanged OnStatueVersionChanged;
    public static MineVersionChanged OnMineVersionChanged;
    public static TreeVersionChanged OnTreeVersionChanged;
    private AudioSource _audioSource;

    private void Update()
    {
    }

    private void Start()
    {
        _currentPlayerData = DataProvider.Instance.PlayerData;
        _resourceData = DataProvider.Instance.ResourceData;

        TreeComponent.OnDropWood += OnDropWood;
        StoneComponent.OnStoneDrop += OnStoneDrop;
        StatueComponent.OnPrayed += OnPrayed;
        EnemyController.OnReducePlayerLifetime += OnReducePlayerLifetime;
        PassingTimeComponent.OnYearPassed += OnYearPassed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;
        StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        StoneUpgradeComponent.OnUpgradeMine += OnUpgradeMine;
        TreeUpgradeComponent.OnUpgradeTree += OnUpgradeTree;

        OnWaveCountChanged?.Invoke(waveCount);
        _audioSource = gameObject.GetComponent<AudioSource>();
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
        DataProvider.Instance.PlayerData.MaxRemainingYears = newAgeValue;
        PlayUpgradingAudioClip();
    }

    private void OnBossDestroyed()
    {
        Wave++;
    }

    private void OnYearPassed()
    {
        _currentPlayerData.CurrentRemainingYears--;
    }

    private void OnReducePlayerLifetime(int amount)
    {
        _currentPlayerData.CurrentRemainingYears -= amount;
    }

    private void OnPrayed(int amount)
    {
        _currentPlayerData.CurrentRemainingYears =
            Math.Min(_currentPlayerData.CurrentRemainingYears + amount, _currentPlayerData.MaxRemainingYears);
    }

    private void OnStoneDrop(int amount)
    {
        _resourceData.StoneAmount += amount;
        uiScript.giveFeedbackWithValues(amount, "Stone");
    }

    private void OnDropWood(int amount)
    {
        _resourceData.WoodAmount += amount;
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