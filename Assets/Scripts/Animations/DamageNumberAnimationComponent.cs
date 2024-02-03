using UnityEngine;

namespace Animations
{
    public class DamageNumberAnimationComponent : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}