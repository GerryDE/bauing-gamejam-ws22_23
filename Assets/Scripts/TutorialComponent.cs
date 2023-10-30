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
        if (data.GetType().Equals(typeof(MoveObjectiveData)))
        {
            _objectiveHandler = new MoveObjectiveHandler((MoveObjectiveData) data);
        }
    }

    private void OnDestroy()
    {
        ObjectiveHandler.OnObjectiveReached -= OnObjectiveReached;
        DataProvider.OnTutorialObjectiveIndexChanged -= OnTutorialObjectiveIndexChanged;
    }
}