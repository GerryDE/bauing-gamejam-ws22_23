using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGaugesUiHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    public void Start()
    {
        SetupSlidersForCurrentWave();

        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;
        DataProvider.OnWaveCountChanged += OnWaveCountChanged;
        WaveHandlerComponent.OnEnemySubWaveDefeated += OnEnemySubWaveDefeated;
    }

    private void OnEnemySubWaveDefeated()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        var data = DataProvider.Instance;
        var currentWaveData = data.CurrentWaveData();
        var subWaveCount = data.SubWaveCount;
        for (int i = 0; i < currentWaveData.subWaves.Count; i++)
        {
            var sliderObj = Instantiate(slider.gameObject);
            sliderObj.transform.SetParent(gameObject.transform);
            sliderObj.GetComponent<Slider>().value = i < currentWaveData.subWaves.Count - 1 - subWaveCount  ? 1 : 0;
            sliderObj.transform.localScale = Vector3.one;
        }
    }

    private void OnBossDestroyed()
    {
        foreach (Slider slider in gameObject.GetComponentsInChildren<Slider>())
        {
            slider.value = 1f;
        }
    }

    private void OnEnemyDestroyed(int objectId)
    {
        var data = DataProvider.Instance;
        var currentSubWave = data.CurrentSubWaveData();
        int offset = 0;
        Slider slider = GetCurrentSlider(offset);
        slider.value = Mathf.Max(0f, slider.value - 1f / currentSubWave.enemies.Count);
    }

    private Slider GetCurrentSlider(int offset)
    {
        var data = DataProvider.Instance;
        return gameObject.transform.GetComponentsInChildren<Slider>()[data.CurrentWaveData().subWaves.Count - data.SubWaveCount + offset - 1];
    }

    private int GetSlidersCount() {
        return gameObject.transform.GetComponentsInChildren<Slider>().Length;
    }

    public void OnWaveCountChanged(int waveCount)
    {
        SetupSlidersForCurrentWave();
    }

    private void SetupSlidersForCurrentWave() 
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        var data = DataProvider.Instance;
        var currentWaveData = data.CurrentWaveData();
        for (int i = 0; i < currentWaveData.subWaves.Count; i++)
        {
            var sliderObj = Instantiate(slider.gameObject);
            sliderObj.transform.SetParent(gameObject.transform);
            sliderObj.GetComponent<Slider>().value = 1f;
            sliderObj.transform.localScale = Vector3.one;
        }
    }

    public void OnDestroy()
    {
        EnemyController.OnEnemyDestroyed -= OnEnemyDestroyed;
        BossComponent.OnBossDestroyed -= OnBossDestroyed;
        DataProvider.OnWaveCountChanged -= OnWaveCountChanged;
        WaveHandlerComponent.OnEnemySubWaveDefeated -= OnEnemySubWaveDefeated;
    }
}
