using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class FinalBossComponent : MonoBehaviour
    {
        public delegate void GameFinished();
        public static GameFinished OnGameFinished;

        private void OnEnable() {
            BossComponent.OnBossDestroyed += OnBossDestroyed;
        }

        private void OnBossDestroyed()
        {
            OnGameFinished?.Invoke();
            SceneManager.LoadScene("WinScreen");
        }

        private void OnDisable() {
            BossComponent.OnBossDestroyed -= OnBossDestroyed;
        }
    }
}