using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowMineUpgradeDataInTextComponent : MonoBehaviour
{
    private enum Version { CurrentVersion, NextVersion };
    private enum Value { Level, DropAmount, MiningDuration, Arrow }

    [SerializeField] private Version version;
    [SerializeField] private Value value;

    private TextMeshProUGUI _textComponent;

    private void OnEnable()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();

        var versionIndex = DataProvider.Instance.CurrentMineVersion;
        if (version.Equals(Version.NextVersion))
        {
            versionIndex++;
        }

        var mineData = DataProvider.Instance.MineData;
        if (mineData.Count <= versionIndex)
        {
            _textComponent.SetText("");
            return;
        }

        var level = mineData.Count <= versionIndex + 1 ? "MAX" : (versionIndex + 1).ToString();
        var currentMineData = mineData[versionIndex];
        var values = new Dictionary<Value, string>
        {
            { Value.Level, level },
            { Value.DropAmount, currentMineData.dropAmount.ToString() },
            { Value.MiningDuration, currentMineData.miningDuaration.ToString() },
            { Value.Arrow, "--->" }
        };

        _textComponent.SetText(values[value]);
    }
}