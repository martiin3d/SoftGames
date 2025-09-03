using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;

    [Tooltip("How often to update the FPS display, in seconds.")]
    [SerializeField] private float _refreshInterval = 0.5f;

    private float _timePassed;
    private int _frameCount;

    private void Update()
    {
        _frameCount++;
        _timePassed += Time.unscaledDeltaTime;

        // Check if enough time has passed to update the display.
        if (_timePassed >= _refreshInterval)
        {
            float fps = _frameCount / _timePassed;
            _fpsText.text = Mathf.RoundToInt(fps).ToString();

            // Reset counters for next interval.
            _frameCount = 0;
            _timePassed = 0f;
        }
    }
}