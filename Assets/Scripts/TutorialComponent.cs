using Data;
using Data.objective;
using Objective;
using UnityEngine;

public class TutorialComponent : MonoBehaviour
{
    public delegate void NewObjectiveStarted(ObjectiveData data);

    public delegate void TutorialCompleted();

    public static TutorialCompleted OnTutorialCompleted;
    public static NewObjectiveStarted OnNewObjectiveStarted;

    [SerializeField] private SkipTutorialData skipTutorialData;

    private ObjectiveHandler _objectiveHandler;

    private void Start()
    {
        _objectiveHandler = null;

        if (skipTutorialData.shallBeSkipped)
        {
            DataProvider.Instance.CurrentTutorialObjectiveIndex = DataProvider.Instance.TutorialObjectives.Count - 1;
        }

        OnTutorialObjectiveIndexChanged(DataProvider.Instance.CurrentTutorialObjectiveIndex);

        ObjectiveHandler.OnObjectiveReached += OnObjectiveReached;
        DynamicObjective.OnDynamicObjectiveStarted += OnDynamicObjectiveStarted;
        DataProvider.OnTutorialObjectiveIndexChanged += OnTutorialObjectiveIndexChanged;
    }

    private void OnDynamicObjectiveStarted(ObjectiveData data)
    {
        SetupObjective(data);
    }

    private void OnObjectiveReached(ObjectiveData data)
    {
        if (DataProvider.Instance.TutorialObjectives.Count > DataProvider.Instance.CurrentTutorialObjectiveIndex + 1)
        {
            DataProvider.Instance.CurrentTutorialObjectiveIndex++;
        }
        else
        {
            OnTutorialCompleted?.Invoke();
            _objectiveHandler = null;
        }
    }

    private void OnTutorialObjectiveIndexChanged(int newIndex)
    {
        SetupObjective(DataProvider.Instance.TutorialObjectives[newIndex]);
    }

    private void SetupObjective(ObjectiveData data)
    {
        if (data.GetType() == typeof(MoveObjectiveData))
        {
            _objectiveHandler = new MoveObjectiveHandler((MoveObjectiveData)data);
        }
        else if (data.GetType() == typeof(CollectResourcesObjectiveData))
        {
            _objectiveHandler = new CollectResourcesObjectiveHandler((CollectResourcesObjectiveData)data);
        }
        else if (data.GetType() == typeof(UpgradeObjectiveData))
        {
            _objectiveHandler = new UpgradeObjectiveHandler((UpgradeObjectiveData)data);
        }
        else if (data.GetType() == typeof(DefeatEnemyObjectiveData))
        {
            _objectiveHandler = new DefeatEnemyObjectiveHandler((DefeatEnemyObjectiveData)data);
        }
        else if (data.GetType() == typeof(TutorialCompletedObjectiveData))
        {
            _objectiveHandler = new TutorialCompletedObjectiveHandler((TutorialCompletedObjectiveData)data);
        }

        OnNewObjectiveStarted?.Invoke(data);
    }

    private void OnDestroy()
    {
        ObjectiveHandler.OnObjectiveReached -= OnObjectiveReached;
        DataProvider.OnTutorialObjectiveIndexChanged -= OnTutorialObjectiveIndexChanged;
    }
}