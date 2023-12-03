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
            Move,
            Collect,
            Upgrade,
            DefeatEnemy,
            Repair,
            Praise
        }

        [SerializeField] private ObjectiveType objectiveType;
        private Dictionary<ObjectiveType, Type> _objectiveTypeDictionary;
        
        private void Awake()
        {
            _objectiveTypeDictionary = new Dictionary<ObjectiveType, Type>()
            {
                { ObjectiveType.Move, typeof(MoveObjectiveData) },
                { ObjectiveType.Collect, typeof(CollectResourcesObjectiveData) },
                { ObjectiveType.Upgrade, typeof(UpgradeObjectiveData) },
                { ObjectiveType.DefeatEnemy, typeof(DefeatEnemyObjectiveData) },
            };

            TutorialComponent.OnNewObjectiveStarted += OnNewObjectiveStarted;
            ObjectiveHandler.OnObjectiveReached += OnObjectiveReached;
        }

        private void OnNewObjectiveStarted(ObjectiveData data)
        {
            if (_objectiveTypeDictionary[objectiveType] != data.GetType()) return;
            foreach (Transform child in gameObject.transform)  
            {
                child.gameObject.SetActive(true);
            }
        }

        private void OnObjectiveReached(ObjectiveData data)
        {
            if (_objectiveTypeDictionary[objectiveType] != data.GetType()) return;
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