using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DamageUiComponent : MonoBehaviour
    {
        [SerializeField] private GameObject enemyDamageObj;
        [SerializeField] private GameObject playerDamageObj;
        [SerializeField] private float yOffset;
        
        private void Start()
        {
            DamageHandlerComponent.OnDealDamageToEnemy += ShowEnemyDamage;
            DamageHandlerComponent.OnDealDamageToPlayer += ShowPlayerDamage;
            DamageHandlerComponent.OnDealDamageToFence += ShowPlayerDamage;
        }
        
        private void ShowPlayerDamage(Transform playerTransform, int damageValue)
        {
            InstantiateDamageUiObject(playerDamageObj, playerTransform.position, damageValue);
        }

        private void ShowEnemyDamage(Transform enemyTransform, int damageValue)
        {
            InstantiateDamageUiObject(enemyDamageObj, enemyTransform.position, damageValue);
        }

        private void InstantiateDamageUiObject(GameObject obj, Vector3 pos, int value)
        {
            var instance = Instantiate(obj, Vector3.zero, Quaternion.identity, transform);
            instance.transform.position += Vector3.up * yOffset;
            instance.transform.position += Vector3.right * pos.x;
            instance.GetComponentInChildren<TextMeshPro>().text = "-" + value;
            instance.SetActive(true);
        }
        
        private void OnDestroy()
        {
            DamageHandlerComponent.OnDealDamageToEnemy -= ShowEnemyDamage;
            DamageHandlerComponent.OnDealDamageToPlayer -= ShowPlayerDamage;
            DamageHandlerComponent.OnDealDamageToFence -= ShowPlayerDamage;
        }
    }
}