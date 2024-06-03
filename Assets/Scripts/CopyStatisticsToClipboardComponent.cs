using Data;
using UnityEngine;

public class CopyStatisticsToClipboardComponent : MonoBehaviour
{
    [SerializeField] private StatisticsData data;

    public void OnClick()
    {
        string str = "";
        str += AppendLine("Play time in seconds", data.PlayTime.ToString());
        str += AppendLine("Total lumber collected", data.LumberAmount.ToString());
        str += AppendLine("Total stones collected", data.StoneAmount.ToString());
        str += AppendLine("Total years regained", data.PrayAmount.ToString());
        str += AppendLine("Fence repairs", data.FenceRepairAmount.ToString());
        str += AppendLine("Total amount of upgrades", data.UpgradeAmount.ToString());
        str += AppendLine("Enemies defeated", data.EnemyDestroyedAmount.ToString());
        str += AppendLine("Waves survived", data.SurvivedWavesAmount.ToString());
        str += AppendLine("Clouds that left the screen", data.CloudsScreenLeftAmount.ToString());

        GUIUtility.systemCopyBuffer = str;
    }

    private string AppendLine(string text, string value)
    {
        return text + ": " + value + "\n";
    }
}