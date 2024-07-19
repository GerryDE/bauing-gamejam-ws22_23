using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGaugesUiHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private List<GameObject> sliderObjs;
    private GameObject _currentSliderObj;

    // Start is called before the first frame update
    public void Start()
    {
        sliderObjs = new List<GameObject>();
        SetupSlidersForCurrentWave();

        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        DataProvider.OnSubWaveCountChanged += OnSubWaveCountChanged;
        DataProvider.OnWaveCountChanged += OnWaveCountChanged;
    }

    private void OnEnemyDestroyed(int objectId) 
    {
        var currentSubWave = DataProvider.Instance.CurrentSubWaveData();
        _currentSliderObj.GetComponent<Slider>().value -= 1f / currentSubWave.enemies.Count;
    }

    public void OnSubWaveCountChanged(int subWaveCount)
    {
        _currentSliderObj.GetComponent<Slider>().value = 0f;
        _currentSliderObj = sliderObjs[subWaveCount];
        _currentSliderObj.GetComponent<Slider>().value = 1f;
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
        Debug.Log(data.WaveCount);
        for (int i = 0; i < currentWaveData.subWaves.Count; i++)
        {
            var sliderObj = Instantiate(slider.gameObject);
            sliderObj.transform.SetParent(gameObject.transform);
            sliderObj.GetComponent<Slider>().value = 1f;
            sliderObj.transform.localScale = Vector3.one;
            sliderObjs.Add(sliderObj);
        }
        _currentSliderObj = sliderObjs[^1];
    }

    public void OnDestroy()
    {
        EnemyController.OnEnemyDestroyed -= OnEnemyDestroyed;
        DataProvider.OnSubWaveCountChanged -= OnSubWaveCountChanged;
        DataProvider.OnWaveCountChanged -= OnWaveCountChanged;
    }
}
