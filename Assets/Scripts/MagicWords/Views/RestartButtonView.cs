using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private Action _onClick;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(Action onButtonClick)
    {
        _onClick = onButtonClick;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _onClick?.Invoke();
    }
}