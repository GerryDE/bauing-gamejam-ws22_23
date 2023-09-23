using Data;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StatisticsTextComponent : MonoBehaviour
{
    private enum StatisticsValue
    {
        PlayTime,
        LumberAmount,
        StoneAmount,
        PrayAmount,
        FenceRepairAmount,
        UpgradeAmount,
        EnemyDestroyedAmount,
        SurvivedWavesAmount,
        CloudsScreenLeftAmount,
    };

    [SerializeField] private StatisticsValue statisticsValue;
    [SerializeField] private StatisticsData data;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Dictionary<StatisticsValue, string> dictionary = new Dictionary<StatisticsValue, string>()
        {
            { StatisticsValue.PlayTime, data.PlayTime.ToString() + " seconds" },
            { StatisticsValue.LumberAmount, data.LumberAmount.ToString() },
            { StatisticsValue.StoneAmount, data.StoneAmount.ToString() },
            { StatisticsValue.PrayAmount, data.PrayAmount.ToString() },
            { StatisticsValue.FenceRepairAmount, data.FenceRepairAmount.ToString() },
            { StatisticsValue.UpgradeAmount, data.UpgradeAmount.ToString() },
            { StatisticsValue.EnemyDestroyedAmount, data.EnemyDestroyedAmount.ToString() },
            { StatisticsValue.SurvivedWavesAmount, data.SurvivedWavesAmount.ToString() },
            { StatisticsValue.CloudsScreenLeftAmount, data.CloudsScreenLeftAmount.ToString() },
        };

        text.SetText(dictionary[statisticsValue]);
    }
}