using System;
using Data.upgradeable_objects.statue;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class StatueNextLevelSpriteComponent : MonoBehaviour
    {
        [SerializeField] private Sprite remainingYearsSprite;
        [SerializeField] private Sprite attackSprite;
        [SerializeField] private Sprite defenseSprite;
        [SerializeField] private Sprite speedSprite;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
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
            _image.sprite = DataProvider.Instance.NextStatueData.statToUpgrade switch
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