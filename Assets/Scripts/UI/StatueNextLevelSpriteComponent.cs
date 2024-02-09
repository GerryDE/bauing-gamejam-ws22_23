using System;
using Data.upgradeable_objects.statue;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StatueNextLevelSpriteComponent : MonoBehaviour
    {
        [SerializeField] private Sprite remainingYearsSprite;
        [SerializeField] private Sprite attackSprite;
        [SerializeField] private Sprite defenseSprite;
        [SerializeField] private Sprite speedSprite;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            UpdateSprite();
            
            StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        }

        private void OnUpgradeStatue(StatueData.UpgradeableStat stat, float value)
        {
            UpdateSprite();
        }

        private void OnEnable()
        {
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            _renderer.sprite = DataProvider.Instance.NextStatueData.statToUpgrade switch
            {
                StatueData.UpgradeableStat.MaxHp => remainingYearsSprite,
                StatueData.UpgradeableStat.Atk => attackSprite,
                StatueData.UpgradeableStat.Def => defenseSprite,
                StatueData.UpgradeableStat.Speed => speedSprite,
                _ => null
            };
        }
        
        private void OnDestroy()
        {
            StatueUpgradeComponent.OnUpgradeStatue -= OnUpgradeStatue;
        }
    }
}