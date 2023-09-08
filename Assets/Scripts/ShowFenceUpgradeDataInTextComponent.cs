using UnityEngine;

public class ShowFenceUpgradeDataInTextComponent : MonoBehaviour
{
    private enum Version { CurrentVersion, NextVersion };
    private enum Value { Level, MaxHp, RepairHp }

    [SerializeField] private Version version;
    [SerializeField] private Value value;
}