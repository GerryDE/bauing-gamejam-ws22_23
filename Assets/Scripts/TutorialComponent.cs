using System;
using Data.objective;
using Objective;
using UnityEngine;

public class TutorialComponent : MonoBehaviour
{
    public delegate void TutorialCompleted();

    public static TutorialCompleted OnTutorialCompleted;

    private ObjectiveHandler _objectiveHandler;
    
    private void Awake()
    {
        _objectiveHandler = null;
        
        OnTutorialObjectiveIndexChanged(0);
        
        ObjectiveHandler.OnObjectiveReached += OnObjectiveReached;
        DataProvider.OnTutorialObjectiveIndexChanged += OnTutorialObjectiveIndexChanged;
    }

    private void OnObjectiveReached(Type type)
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
            _objectiveHandler = new MoveObjectiveHandler((MoveObjectiveData) data);
        }
        else if (data.GetType() == typeof(CollectResourcesObjectiveData))
        {
            _objectiveHandler = new CollectResourcesObjectiveHandler((CollectResourcesObjectiveData) data);
        }
    }

    private void OnDestroy()
    {
        ObjectiveHandler.OnObjectiveReached -= OnObjectiveReached;
        DataProvider.OnTutorialObjectiveIndexChanged -= OnTutorialObjectiveIndexChanged;
    }
}