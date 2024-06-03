using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowFenceUpgradeDataInTextComponent : MonoBehaviour
{
    private enum Version { CurrentVersion, NextVersion };
    private enum Value { Level, MaxHp, RepairHp, Arrow }

    [SerializeField] private int index;
    [SerializeField] private Version version;
    [SerializeField] private Value value;

    private TextMeshProUGUI _textComponent;

    private void OnEnable()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();

        var fenceData = DataProvider.Instance.FenceData[index];
        var versionIndex = fenceData.version;
        if (version.Equals(Version.NextVersion))
        {
            versionIndex++;
        }

        if (fenceData.data.Count <= versionIndex)
        {
            _textComponent.SetText("");
            return;
        }

        var level = fenceData.data.Count <= versionIndex + 1 ? "MAX" : (versionIndex + 1).ToString();
        var values = new Dictionary<Value, string>
        {
            { Value.Level, level },
            { Value.MaxHp, fenceData.data[versionIndex].maxHp.ToString() },
            { Value.RepairHp, fenceData.data[versionIndex].repairHealAmount.ToString() },
            { Value.Arrow, "--->" }
        };

        _textComponent.SetText(values[value]);
    }
}