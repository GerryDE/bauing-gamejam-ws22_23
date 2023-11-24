using System;
using System.Collections.Generic;
using Data.objective;
using Objective;
using UnityEngine;

namespace UI
{
    public class EnableOnActiveObjectiveOnly : MonoBehaviour
    {
        private enum ObjectiveType
        {
            Move
        }

        [SerializeField] private ObjectiveType objectiveType;
        private Dictionary<ObjectiveType, Type> _objectiveTypeDictionary;
        
        private void Awake()
        {
            _objectiveTypeDictionary = new Dictionary<ObjectiveType, Type>()
            {
                {
                    ObjectiveType.Move, typeof(MoveObjectiveData)
                }
            };

            TutorialComponent.OnNewObjectiveStarted += OnNewObjectiveStarted;
            ObjectiveHandler.OnObjectiveReached += OnObjectiveReached;
        }

        private void OnNewObjectiveStarted(Type type)
        {
            if (_objectiveTypeDictionary[objectiveType] != type) return;
            foreach (Transform child in gameObject.transform)  
            {
                child.gameObject.SetActive(true);
            }
        }

        private void OnObjectiveReached(Type type)
        {
            if (_objectiveTypeDictionary[objectiveType] != type) return;
            foreach (Transform child in gameObject.transform)  
            {
                child.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            TutorialComponent.OnNewObjectiveStarted -= OnNewObjectiveStarted;
            ObjectiveHandler.OnObjectiveReached -= OnObjectiveReached;
        }
    }
}