using System;
using Data;
using DefaultNamespace;
using UnityEngine;

public class StatisticsDataComponent : MonoBehaviour
{
    // Make sure that this component only exists once in the project to the keep the Singleton approach
    public static StatisticsDataComponent Instance { get; private set; }

    [SerializeField] private StatisticsData data;
    private StatisticsData _tempData;

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

        CheckForGameOverComponent.OnGameOver += WriteDataIntoFile;
        FinalBossComponent.OnGameFinished += WriteDataIntoFile;

        _tempData = new StatisticsData();
    }

    private void OnResetCloudPosition()
    {
        _tempData.CloudsScreenLeftAmount++;
    }

    private void OnUpgradeStatue(int newAgeValue, Sprite sprite)
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
        _tempData.FenceRepairAmount++;
    }

    private void OnBossDestroyed()
    {
        _tempData.SurvivedWavesAmount++;
    }

    private void OnEnemyDestroyed(int objectId)
    {
        _tempData.EnemyDestroyedAmount++;
    }

    private void OnPrayed(int amount)
    {
        _tempData.PrayAmount += amount;
    }

    private void OnStoneDrop(int amount)
    {
        _tempData.StoneAmount += amount;
    }

    private void OnDropWood(int amount)
    {
        _tempData.LumberAmount += amount;
    }

    private void IncrementUpgradeData()
    {
        _tempData.UpgradeAmount++;
    }

    private void WriteDataIntoFile()
    {
        data.LumberAmount = _tempData.LumberAmount;
        data.StoneAmount = _tempData.StoneAmount;
        data.PrayAmount = _tempData.PrayAmount;
        data.EnemyDestroyedAmount = _tempData.EnemyDestroyedAmount;
        data.SurvivedWavesAmount = _tempData.SurvivedWavesAmount;
        data.PlayTime = Time.time;
        data.CloudsScreenLeftAmount = _tempData.CloudsScreenLeftAmount;
        data.FenceRepairAmount = _tempData.FenceRepairAmount;
        data.UpgradeAmount = _tempData.UpgradeAmount;
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

        CheckForGameOverComponent.OnGameOver -= WriteDataIntoFile;
        FinalBossComponent.OnGameFinished -= WriteDataIntoFile;
    }
}