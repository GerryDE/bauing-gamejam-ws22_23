using System;
using UnityEngine;
using static Data.StatueData;

public class DataHandlerComponent : MonoBehaviour
{
    private DataProvider.CurrentPlayerData _currentPlayerData;
    private DataProvider.CurrentResourceData _resourceData;

    [SerializeField] AfterEffects postProcessingCameraScript;
    [SerializeField] UIController uiScript;

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
        PassingTimeComponent.OnYearPassed += OnYearPassed;
        StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        StoneUpgradeComponent.OnUpgradeMine += OnUpgradeMine;
        TreeUpgradeComponent.OnUpgradeTree += OnUpgradeTree;

        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnUpgradeTree()
    {
        DataProvider.Instance.CurrentTreeVersion++;
        PlayUpgradingAudioClip();
    }

    private void OnUpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite)
    {
        DataProvider.Instance.CurrentMineVersion++;
        PlayUpgradingAudioClip();
    }

    private void OnUpgradeStatue(UpgradeableStat stat, float value)
    {
        var data = DataProvider.Instance.PlayerData;
        switch (stat)
        {
            case UpgradeableStat.MaxHp:
                DataProvider.Instance.PlayerData.MaxRemainingYears = (int) value;
                break;
            case UpgradeableStat.Atk: data.AttackValue = (int) value;
                break;
            case UpgradeableStat.Def: data.DefenseValue = (int) value;
                break;
            case UpgradeableStat.Speed: data.MoveSpeed = value;
                break;
        }
        PlayUpgradingAudioClip();
    }

    private void OnYearPassed()
    {
        _currentPlayerData.CurrentRemainingYears--;
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
        PassingTimeComponent.OnYearPassed -= OnYearPassed;
        StatueUpgradeComponent.OnUpgradeStatue -= OnUpgradeStatue;
        StoneUpgradeComponent.OnUpgradeMine -= OnUpgradeMine;
        TreeUpgradeComponent.OnUpgradeTree -= OnUpgradeTree;
    }
}