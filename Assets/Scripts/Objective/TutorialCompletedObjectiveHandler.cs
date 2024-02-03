using Data.objective;

namespace Objective
{
    public class TutorialCompletedObjectiveHandler : ObjectiveHandler
    {
        private TutorialCompletedObjectiveData _data;

        public TutorialCompletedObjectiveHandler(TutorialCompletedObjectiveData data)
        {
            _data = data;
            DataProvider.Instance.Wave = 1;
            ResetData();
        }

        private static void ResetData()
        {
            var dataProvider = DataProvider.Instance;
            dataProvider.ResourceData.WoodAmount = dataProvider.InitialResourceData.WoodAmount;
            dataProvider.ResourceData.StoneAmount = dataProvider.InitialResourceData.StoneAmount;
            dataProvider.CurrentFenceVersion = 0;
            dataProvider.CurrentTreeVersion = 0;
            dataProvider.CurrentMineVersion = 0;
            dataProvider.CurrentStatueVersion = 0;
            dataProvider.PlayerData.MaxRemainingYears = dataProvider.initialCurrentPlayerData.maxRemainingYears;
            dataProvider.PlayerData.CurrentRemainingYears = dataProvider.PlayerData.MaxRemainingYears;
        }
    }
}