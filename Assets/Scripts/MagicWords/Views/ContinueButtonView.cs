using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// View component that exposes a "continue" button and tracks whether it has been pressed.
/// </summary>
public class ContinueButtonView : MonoBehaviour, IContinueButtonView
{
    [SerializeField] private Button _button;

    /// <inheritdoc/>
    public bool IsContinueButtonPressed { get; private set; }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    /// <inheritdoc/>
    public void ResetState()
    {
        _button.interactable = true;
        IsContinueButtonPressed = false;
    }

    private void OnClick()
    {
        _button.interactable = false;
        IsContinueButtonPressed = true;
    }
}
