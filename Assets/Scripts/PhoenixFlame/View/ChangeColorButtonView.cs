using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private Animator _animator;
    private const string TRIGGER_EVENT_NAME = "TriggerColorChange";

    public void Initialize(Animator animator)
    {
        _animator = animator;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _animator.SetTrigger(TRIGGER_EVENT_NAME);
    }
}
