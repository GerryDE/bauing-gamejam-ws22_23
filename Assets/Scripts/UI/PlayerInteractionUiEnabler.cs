using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PlayerInteractionUiEnabler : MonoBehaviour
    {
        [System.Serializable]
        public struct ObjectLayer
        {
            public string layer;
            public GameObject gameObject;
        }

        [SerializeField] private List<ObjectLayer> uiObjects;

        private void OnTriggerStay2D(Collider2D other)
        {
            foreach (var obj in uiObjects)
            {
                obj.gameObject.SetActive(obj.layer == 
                                         LayerMask.LayerToName(other.gameObject.layer));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            bool interactableExited = false;
            foreach (var obj in uiObjects)
            {
                if (LayerMask.LayerToName(other.gameObject.layer) == obj.layer)
                {
                    interactableExited = true;
                    break;
                }
            }

            if (interactableExited)
            {
                foreach (var obj in uiObjects)
                {
                    obj.gameObject.SetActive(false);
                }
            }
        }
    }
}
