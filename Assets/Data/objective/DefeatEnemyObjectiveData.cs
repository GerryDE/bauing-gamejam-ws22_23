using System;
using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/DefeatEnemyObjectiveData", menuName = "Data/DefeatEnemyObjectiveData")]
    public class DefeatEnemyObjectiveData : ObjectiveData
    {
        public int enemiesToKill = 1;
        public GameObject enemyObj;
        public override string GetObjectiveText()
        {
            var enemyTerm = enemiesToKill == 1 ? "enemy" : "enemies";
            return $"Defeat {enemiesToKill} {enemyTerm} by running into them";
        }
    }
}
