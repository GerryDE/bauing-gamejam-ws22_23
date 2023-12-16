using System;
using System.Collections.Generic;
using Objective;
using UnityEngine;
using TMPro;
using Data.objective;

namespace UI
{
    public class TutorialObjectivesUiComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textComponent;
        [SerializeField, Range(0f, 1f)] private float objectiveReachedTransparency;
        [SerializeField] private float finishedTextSize = 12f;

        private Dictionary<Type, List<TextMeshProUGUI>> _objectsForType;

        private void Start()
        {
            _objectsForType = new Dictionary<Type, List<TextMeshProUGUI>>();

            OnTutorialObjectiveIndexChanged(0);

            DynamicObjective.OnDynamicObjectiveStarted += OnDynamicObjectiveStarted;
            ObjectiveHandler.OnObjectiveReached += OnObjectiveReached;
            DataProvider.OnTutorialObjectiveIndexChanged += OnTutorialObjectiveIndexChanged;
        }

        private void OnDynamicObjectiveStarted(ObjectiveData data)
        {
            var obj = Instantiate(textComponent.gameObject, gameObject.transform);
            var instanceTextComponent = obj.GetComponent<TextMeshProUGUI>();
            instanceTextComponent.text = data.GetObjectiveText();
            if (!_objectsForType.ContainsKey(data.GetType()))
            {
                _objectsForType.Add(data.GetType(), new List<TextMeshProUGUI>());
            }
            
            _objectsForType[data.GetType()].Add(instanceTextComponent);
        }

        private void OnObjectiveReached(ObjectiveData data)
        {
            Type type = data.GetType();
            if (!_objectsForType.ContainsKey(type)) return;
            foreach (var comp in _objectsForType[type])
            {
                if (comp.text.StartsWith("<s>")) continue;
                var text = "<s>" + comp.text + "</s>";
                comp.text = text;
                comp.fontSize = finishedTextSize;
                comp.color = new Color(comp.color.r, comp.color.g, comp.color.b, objectiveReachedTransparency);
            }
        }

        private void OnTutorialObjectiveIndexChanged(int newIndex)
        {
            var data = DataProvider.Instance.TutorialObjectives[newIndex];
            var obj = Instantiate(textComponent.gameObject, gameObject.transform);
            var instanceTextComponent = obj.GetComponent<TextMeshProUGUI>();
            instanceTextComponent.text = data.GetObjectiveText();

            if (!_objectsForType.ContainsKey(data.GetType()))
            {
                _objectsForType.Add(data.GetType(), new List<TextMeshProUGUI>());
            }
            
            _objectsForType[data.GetType()].Add(instanceTextComponent);
        }
    }
}