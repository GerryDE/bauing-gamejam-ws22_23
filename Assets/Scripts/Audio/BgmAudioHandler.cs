using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    public class BgmAudioHandler : MonoBehaviour
    {
        private enum CurrentBgm
        {
            DEFAULT,
            ENEMY,
            BOSS
        }

        [SerializeField] private AudioSource audioSource1;
        [SerializeField] private AudioSource audioSource2;
        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private float maxVolume = 0.5f;

        private AudioSource _mainAudioSource;
        private AudioSource _fadeAudioSource;
        
        private CurrentBgm _currentBgm = CurrentBgm.DEFAULT;
        
        void Start()
        {
            _mainAudioSource = audioSource1;
            _mainAudioSource.clip = DataProvider.Instance.DefaultBgm;
            _mainAudioSource.volume = maxVolume;
            _mainAudioSource.Play();

            _fadeAudioSource = audioSource2;
            
            WaveHandlerComponent.OnSpawnEnemy += OnSpawnEnemy;
        }

        private void OnSpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies, int killedEnemies, int enemiesToKillUntilBoss)
        {
            if (enemyPrefab.GetComponent<BossComponent>() != null && _currentBgm != CurrentBgm.BOSS)
            {
                _currentBgm = CurrentBgm.BOSS;
                Fade(DataProvider.Instance.BossBgm);
            }
            else if (_currentBgm != CurrentBgm.ENEMY)
            {
                _currentBgm = CurrentBgm.ENEMY;
                Fade(DataProvider.Instance.EnemyBgm);
            }
        }

        private void Fade(AudioClip newAudioClip)
        {
            _fadeAudioSource.clip = newAudioClip;
            _fadeAudioSource.Play();
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            var timeElapsed = 0f;
            while (_mainAudioSource.volume > 0f)
            {
                _mainAudioSource.volume = Mathf.Lerp(maxVolume, 0f, timeElapsed / fadeDuration);
                _fadeAudioSource.volume = Mathf.Lerp(0f, maxVolume, timeElapsed / fadeDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            (_mainAudioSource, _fadeAudioSource) = (_fadeAudioSource, _mainAudioSource);
        }

        private void OnDestroy()
        {
            WaveHandlerComponent.OnSpawnEnemy -= OnSpawnEnemy;
        }
    }
}
