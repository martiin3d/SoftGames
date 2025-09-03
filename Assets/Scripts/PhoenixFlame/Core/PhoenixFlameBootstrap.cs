using UnityEngine;

/// <summary>
/// Bootstrap component for the Phoenix Flame scene that wires the color-changing button to the Animator.
/// </summary>
public class PhoenixFlameBootstrap : MonoBehaviour
{
    [SerializeField] private ChangeColorButtonView _changeColorButton;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _changeColorButton.Initialize(_animator);
    }
}
