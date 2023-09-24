using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class FinalBossComponent : MonoBehaviour
    {
        public delegate void GameFinished();
        public static GameFinished OnGameFinished;

        private void OnDestroy()
        {
            OnGameFinished?.Invoke();
            SceneManager.LoadScene("WinScreen");
        }
    }
}