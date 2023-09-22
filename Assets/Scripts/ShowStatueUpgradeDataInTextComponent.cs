using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowStatueUpgradeDataInTextComponent : MonoBehaviour
{
    private enum Version { CurrentVersion, NextVersion };
    private enum Value { Level, MaxAge, Arrow }

    [SerializeField] private Version version;
    [SerializeField] private Value value;

    private TextMeshProUGUI _textComponent;

    private void OnEnable()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();

        var versionIndex = DataProvider.Instance.CurrentStatueVersion;
        if (version.Equals(Version.NextVersion))
        {
            versionIndex++;
        }

        var statueData = DataProvider.Instance.StatueData;
        if (statueData.Count <= versionIndex)
        {
            _textComponent.SetText("");
            return;
        }

        var level = statueData.Count <= versionIndex + 1 ? "MAX" : (versionIndex + 1).ToString();
        var currentStatueData = statueData[versionIndex];
        var values = new Dictionary<Value, string>
        {
            { Value.Level, level },
            { Value.MaxAge, currentStatueData.maxAge.ToString() },
            { Value.Arrow, "--->" }
        };

        _textComponent.SetText(values[value]);
    }
}