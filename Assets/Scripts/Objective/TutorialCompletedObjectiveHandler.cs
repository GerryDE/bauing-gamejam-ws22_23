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
            dataProvider.ResourceData.WoodAmount = 0;
            dataProvider.ResourceData.StoneAmount = 0;
            foreach (var fenceDataIndex in dataProvider.FenceData)
            {
                fenceDataIndex.version = 0;
            }
            dataProvider.CurrentTreeVersion = 0;
            dataProvider.CurrentMineVersion = 0;
            dataProvider.CurrentStatueVersion = 0;
            dataProvider.PlayerData.MaxRemainingYears = dataProvider.StatueData[0].maxAge;
            dataProvider.PlayerData.CurrentRemainingYears = dataProvider.PlayerData.MaxRemainingYears;
        }
    }
}