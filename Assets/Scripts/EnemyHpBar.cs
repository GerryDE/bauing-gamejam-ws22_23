using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyHpBar : MonoBehaviour
    {
        
        private SpriteRenderer _foregroundRenderer;

        private void Start()
        {
            _foregroundRenderer = GetComponent<SpriteRenderer>();
        }

        public void UpdateValues(float currentTime, float requiredTime)
        {
            _foregroundRenderer.gameObject.transform.localScale = new Vector3(Mathf.Min(currentTime / requiredTime, 1f),
                _foregroundRenderer.gameObject.transform.localScale.y, 0f);
        }

        public void Enable()
        {
            _foregroundRenderer.enabled = true;
        }

        public void Disable()
        {
            _foregroundRenderer.enabled = false;
        }
    }
}