using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int maxRemainingYears = 50;
        public int currentRemainingYears = 50;

        public int attack = 1;

        public int defense = 1;

        [Tooltip("The initial max move speed value when the Player has the maximum amount of remaining years")]
        public float moveSpeed = 300f;

        [Tooltip("The initial amount of remaining years that is required to start transition from young to old")]
        public int remainingYearsForStayingYoung = 40;

        [Tooltip("The initial amount of remaining years that is required to finish transition from young to old")]
        public int remainingYearsForBecomingOld = 10;

        [Tooltip(
             "The initial minimal move speed percentage value after the transition to old has been finished. " +
             "For example, when move speed is 300 and the speed percentage value is 0.2, the Player's move speed " +
             "will be 60 after finishing the transition to become old."),
         Range(0f, 1f)]
        public float minSpeedPercentage = 0.2f;

        [Tooltip("The force that shall be performed on the Player to throw it back after getting hit by an enemy.")]
        public Vector2 throwBackForce = new Vector2(500f, 0f);
    }
}