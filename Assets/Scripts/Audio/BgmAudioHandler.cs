using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    public class BgmAudioHandler : MonoBehaviour
    {
        private enum CurrentBgm
        {
            DEFAULT,
            ENEMY,
            BOSS,
            FINAL_BOSS
        }

        [SerializeField] private AudioSource audioSource1;
        [SerializeField] private AudioSource audioSource2;
        [SerializeField] private float fadeOutDuration = 2f;
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private float fadeInDelay = 1f;

        private float _maxVolume = 0.5f;
        private AudioSource _mainAudioSource;
        private AudioSource _fadeAudioSource;
        
        private CurrentBgm _currentBgm = CurrentBgm.DEFAULT;
        
        public void Start()
        {
            Debug.Log("Handler started");
            _mainAudioSource = audioSource1;
            _mainAudioSource.clip = DataProvider.Instance.DefaultBgm;
            _maxVolume = GetMaxVolume(DataProvider.Instance.VolumeData);
            audioSource1.volume = _maxVolume;
            audioSource2.volume = 0f;
            _mainAudioSource.volume = _maxVolume;
            _mainAudioSource.Play();

            _fadeAudioSource = audioSource2;
            
            WaveHandlerComponent.OnSpawnEnemy += OnSpawnEnemy;
            WaveHandlerComponent.OnEnemySubWaveDefeated += OnEnemySubWaveDefeated;
            AudioVolumeData.OnVolumeChanged += OnVolumeChanged;
        }

        private void OnVolumeChanged(float volume)
        {
            Debug.Log("Volume changed");
            _maxVolume = GetMaxVolume(DataProvider.Instance.VolumeData);
            audioSource1.volume = audioSource1 == _mainAudioSource ? volume : 0f;
            audioSource2.volume = audioSource2 == _mainAudioSource ? volume : 0f;
        }

        private void OnSpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies)
        {
            Debug.Log("Enemy spawned");
            if (enemyPrefab.GetComponent<FinalBossComponent>() != null && _currentBgm != CurrentBgm.FINAL_BOSS)
            {
                _currentBgm = CurrentBgm.FINAL_BOSS;
                Fade(DataProvider.Instance.FinalBossBgm);
            }
            else if (enemyPrefab.GetComponent<BossComponent>() != null && _currentBgm != CurrentBgm.BOSS)
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
        
        private void OnEnemySubWaveDefeated()
        {
            Debug.Log("Enemy sub wave defeated");
            if (_currentBgm == CurrentBgm.DEFAULT) return;
            _currentBgm = CurrentBgm.DEFAULT;
            Fade(DataProvider.Instance.DefaultBgm);
        }

        private void Fade(AudioClip newAudioClip)
        {
            _fadeAudioSource.clip = newAudioClip;
            _fadeAudioSource.Play();
            StartCoroutine(FadeOut());
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeOut()
        {
            var timeElapsed = 0f;
            while (_mainAudioSource.volume > 0f)
            {
                _mainAudioSource.volume = Vector3.Slerp(_maxVolume * Vector3.right, 0f * Vector3.right, timeElapsed / fadeOutDuration).x;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        
        private IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(fadeInDelay);
            
            var timeElapsed = 0f;
            while (_fadeAudioSource.volume < _maxVolume)
            {
                _fadeAudioSource.volume = Vector3.Slerp(0f * Vector3.right, _maxVolume * Vector3.right, timeElapsed / fadeInDuration).x;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            (_mainAudioSource, _fadeAudioSource) = (_fadeAudioSource, _mainAudioSource);
        }

        private float GetMaxVolume(AudioVolumeData volumeData) {
            return volumeData.Volume;
        }

        public void OnDestroy()
        {
            WaveHandlerComponent.OnSpawnEnemy += OnSpawnEnemy;
            WaveHandlerComponent.OnEnemySubWaveDefeated += OnEnemySubWaveDefeated;
            AudioVolumeData.OnVolumeChanged += OnVolumeChanged;
        }
    }
}
