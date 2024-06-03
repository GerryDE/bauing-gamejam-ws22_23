using UnityEngine;

public class KeepUiOnScreenComponent : MonoBehaviour
{
    [SerializeField] private float xScale;
    [SerializeField] private float y;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        var minX = horzExtent - xScale / 2.0;
        var maxX = xScale / 2.0 - horzExtent;
        //var minY = vertExtent - mapY / 2.0;
        //var maxY = mapY / 2.0 - vertExtent;

        _rectTransform.position = new Vector3(
            Mathf.Clamp(_rectTransform.rect.x, (float) minX, (float)(maxX - _rectTransform.rect.width)), 
            y);
    }
}
