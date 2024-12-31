using System;
using UnityEngine;

namespace Objective
{
    public class DynamicObjectiveComponent : MonoBehaviour
    {
        private DynamicObjectiveHandler _handler;
        private void OnEnable()
        {
            _handler = new DynamicObjectiveHandler(DataProvider.Instance.DynamicObjectives);
        }

        private void OnDisable()
        {
            _handler = null;
        }
    }
}