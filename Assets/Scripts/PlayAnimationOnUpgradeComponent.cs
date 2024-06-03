using System;
using Data.upgradeable_objects.statue;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class PlayAnimationOnUpgradeComponent : MonoBehaviour
{
    [SerializeField] private LevelUpAnimationTarget target;
    private Animation _levelUpAnimation;

    private void Awake()
    {
        _levelUpAnimation = GetComponent<Animation>();
        
        switch (target)
        {
            case LevelUpAnimationTarget.PLAYER:
                StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
                break;
            case LevelUpAnimationTarget.FENCE:
                FenceUpgradeComponent.OnUpgradeFence += OnUpgradeFence;
                break;
            case LevelUpAnimationTarget.FENCE_2:
                FenceUpgradeComponent.OnUpgradeFence += OnUpgradeFence2;
                break;
            case LevelUpAnimationTarget.MINE:
                StoneUpgradeComponent.OnUpgradeMine += OnUpgradeMine;
                break;
            case LevelUpAnimationTarget.TREE:
                TreeUpgradeComponent.OnUpgradeTree += OnUpgradeTree;
                break;
            default:
                throw new Exception("Unknown LevelUp Animation Target");
        }
    }

    private void OnUpgradeTree()
    {
        PlayLevelUpAnimation();
    }

    private void OnUpgradeMine(float newminingduration, int newdropamount, Sprite sprite)
    {
        PlayLevelUpAnimation();
    }

    private void OnUpgradeStatue(StatueData.UpgradeableStat stat, float value)
    {
        PlayLevelUpAnimation();
    }
    
    private void OnUpgradeFence(int index, int newhpvalue, int newdamage, Sprite sprite)
    {
        if (index != 0) return;
        PlayLevelUpAnimation();
    }
    
    private void OnUpgradeFence2(int index, int newhpvalue, int newdamage, Sprite sprite)
    {
        if (index != 1) return;
        PlayLevelUpAnimation();
    }

    private void PlayLevelUpAnimation()
    {
        _levelUpAnimation.Play();
    }

    private void OnDestroy()
    {
        StatueUpgradeComponent.OnUpgradeStatue -= OnUpgradeStatue;
        FenceUpgradeComponent.OnUpgradeFence -= OnUpgradeFence;
        FenceUpgradeComponent.OnUpgradeFence -= OnUpgradeFence2;
        StoneUpgradeComponent.OnUpgradeMine -= OnUpgradeMine;
        TreeUpgradeComponent.OnUpgradeTree -= OnUpgradeTree;
    }
}