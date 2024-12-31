using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGaugesUiHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    public void OnEnable()
    {
        SetupSlidersForCurrentWave();

        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;
        DataProvider.OnWaveCountChanged += OnWaveCountChanged;
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
        var currentSubWaveCount = data.SubWaveCount;
        int offset = 0;
        if (data.CurrentWaveData().subWaves.Count - currentSubWaveCount < GetSlidersCount()
            && GetCurrentSlider(1).value > 0f) {
                offset = 1;
            }
        Slider slider = GetCurrentSlider(offset);
        slider.value = Mathf.Max(0f, slider.value - 1f / currentSubWave.enemies.Count);
        if (slider.value < 1f / currentSubWave.enemies.Count) {
            slider.value = 0f;
        }
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

    public void OnDisable()
    {
        EnemyController.OnEnemyDestroyed -= OnEnemyDestroyed;
        BossComponent.OnBossDestroyed -= OnBossDestroyed;
        DataProvider.OnWaveCountChanged -= OnWaveCountChanged;
    }
}
