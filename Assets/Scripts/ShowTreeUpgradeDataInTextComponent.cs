using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowTreeUpgradeDataInTextComponent : MonoBehaviour
{
    private enum Version { CurrentVersion, NextVersion };
    private enum Value { Level, DropAmount, Arrow }

    [SerializeField] private Version version;
    [SerializeField] private Value value;

    private TextMeshProUGUI _textComponent;

    private void OnEnable()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();

        var versionIndex = DataProvider.Instance.CurrentTreeVersion;
        if (version.Equals(Version.NextVersion))
        {
            versionIndex++;
        }

        var treeData = DataProvider.Instance.TreeData;
        if (treeData.Count <= versionIndex)
        {
            _textComponent.SetText("");
            return;
        }

        var level = treeData.Count <= versionIndex + 1 ? "MAX" : (versionIndex + 1).ToString();
        var currentTreeData = treeData[versionIndex];
        var dropAmount = currentTreeData.smallDropAmount + "-" + currentTreeData.largeDropAmount;
        var values = new Dictionary<Value, string>
        {
            { Value.Level, level },
            { Value.DropAmount, dropAmount },
            { Value.Arrow, "--->" }
        };

        _textComponent.SetText(values[value]);
    }
}