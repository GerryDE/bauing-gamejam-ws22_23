using System;
using Data;
using DefaultNamespace;
using UnityEngine;

public class StatisticsDataComponent : MonoBehaviour
{
    // Make sure that this component only exists once in the project to the keep the Singleton approach
    public static StatisticsDataComponent Instance { get; private set; }

    [SerializeField] private StatisticsData data;

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

        TreeComponent.OnDropWood += OnDropWood;
        StoneComponent.OnStoneDrop += OnStoneDrop;
        StatueComponent.OnPrayed += OnPrayed;
        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;
        FenceRepairComponent.OnRepairFence += OnRepairFence;
        FenceUpgradeComponent.OnUpgradeFence += OnUpgradeFence;
        TreeUpgradeComponent.OnUpgradeTree += OnUpgradeTree;
        StoneUpgradeComponent.OnUpgradeMine += OnUpgradeMine;
        StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        MovingCloudComponent.OnResetCloudPosition += OnResetCloudPosition;

        CheckForGameOverComponent.OnGameOver += WritePlayTime;
        FinalBossComponent.OnGameFinished += WritePlayTime;

        ResetData();
    }

    private void OnResetCloudPosition()
    {
        data.CloudsScreenLeftAmount++;
    }

    private void OnUpgradeStatue(StatueData.UpgradeableStat stat, float value)
    {
        IncrementUpgradeData();
    }

    private void OnUpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite)
    {
        IncrementUpgradeData();
    }

    private void OnUpgradeTree()
    {
        IncrementUpgradeData();
    }

    private void OnUpgradeFence(int newHpValue, int newDamage, Sprite sprite)
    {
        IncrementUpgradeData();
    }

    private void OnRepairFence(int amount)
    {
        data.FenceRepairAmount++;
    }

    private void OnBossDestroyed()
    {
        data.SurvivedWavesAmount++;
    }

    private void OnEnemyDestroyed(int objectId)
    {
        data.EnemyDestroyedAmount++;
    }

    private void OnPrayed(int amount)
    {
        data.PrayAmount += amount;
    }

    private void OnStoneDrop(int amount)
    {
        data.StoneAmount += amount;
    }

    private void OnDropWood(int amount)
    {
        data.LumberAmount += amount;
    }

    private void IncrementUpgradeData()
    {
        data.UpgradeAmount++;
    }

    private void WritePlayTime()
    {
        data.PlayTime = Time.time;
    }

    private void ResetData()
    {
        data.PlayTime = 0f;
        data.LumberAmount = 0;
        data.StoneAmount = 0;
        data.PrayAmount = 0;
        data.EnemyDestroyedAmount = 0;
        data.SurvivedWavesAmount = 0;
        data.CloudsScreenLeftAmount = 0;
        data.FenceRepairAmount = 0;
        data.UpgradeAmount = 0;
    }

    private void OnDestroy()
    {
        TreeComponent.OnDropWood -= OnDropWood;
        StoneComponent.OnStoneDrop -= OnStoneDrop;
        StatueComponent.OnPrayed -= OnPrayed;
        EnemyController.OnEnemyDestroyed -= OnEnemyDestroyed;
        BossComponent.OnBossDestroyed -= OnBossDestroyed;
        FenceRepairComponent.OnRepairFence -= OnRepairFence;
        FenceUpgradeComponent.OnUpgradeFence -= OnUpgradeFence;
        TreeUpgradeComponent.OnUpgradeTree -= OnUpgradeTree;
        StoneUpgradeComponent.OnUpgradeMine -= OnUpgradeMine;
        StatueUpgradeComponent.OnUpgradeStatue -= OnUpgradeStatue;
        MovingCloudComponent.OnResetCloudPosition -= OnResetCloudPosition;

        CheckForGameOverComponent.OnGameOver -= WritePlayTime;
        FinalBossComponent.OnGameFinished -= WritePlayTime;
    }
}