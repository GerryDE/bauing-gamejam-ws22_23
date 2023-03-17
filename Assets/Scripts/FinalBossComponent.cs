using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class FinalBossComponent : MonoBehaviour
    {
        private void OnDestroy()
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}