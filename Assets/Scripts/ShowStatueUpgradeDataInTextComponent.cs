using System.Collections.Generic;
using Data;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowStatueUpgradeDataInTextComponent : MonoBehaviour
{
    private enum Version { CurrentVersion, NextVersion };
    private enum Value { Level, StatValue, Arrow }

    [SerializeField] private Version version;
    [SerializeField] private Value value;

    private TextMeshProUGUI _textComponent;

    private void OnEnable()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();

        var versionIndex = DataProvider.Instance.CurrentStatueVersion;
        var nextStatueData = DataProvider.Instance.NextStatueData;

        
        float statValue;
        if (version.Equals(Version.NextVersion))
        {
            statValue = nextStatueData.statValue;
        }
        else
        {
            var statType = nextStatueData.statToUpgrade;
            var playerData = DataProvider.Instance.PlayerData;
            switch (statType)
            {
                case StatueData.UpgradeableStat.MaxHp: statValue = playerData.MaxRemainingYears;
                    break;
                case StatueData.UpgradeableStat.Atk: statValue = playerData.AttackValue;
                    break;
                case StatueData.UpgradeableStat.Def: statValue = playerData.DefenseValue;
                    break;
                case StatueData.UpgradeableStat.Speed: statValue = playerData.MoveSpeed;
                    break;
                default:
                    statValue = 0;
                    break;
            }
        }

        var statueData = DataProvider.Instance.NextStatueData;

        var level = (versionIndex + 1).ToString();
        var values = new Dictionary<Value, string>
        {
            { Value.Level, level },
            { Value.StatValue, statValue.ToString() },
            { Value.Arrow, "--->" }
        };

        _textComponent.SetText(values[value]);
    }
}