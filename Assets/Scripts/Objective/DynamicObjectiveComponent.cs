using System;
using UnityEngine;

namespace Objective
{
    public class DynamicObjectiveComponent : MonoBehaviour
    {
        private DynamicObjectiveHandler _handler;
        private void Start()
        {
            _handler = new DynamicObjectiveHandler(DataProvider.Instance.DynamicObjectives);
        }

        private void OnDestroy()
        {
            _handler = null;
        }
    }
}