using System;
using UnityEngine;
using UnityEngine.UI;

public class ReplayButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private IDeckManager _deckManager;

    public void Initialize(IDeckManager deckManager)
    {
        _deckManager = deckManager;
        _deckManager.OnRestartFinished += OnRestartFinished;
    }

    private void OnDestroy()
    {
        _deckManager.OnRestartFinished -= OnRestartFinished;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnRestartFinished()
    {
        _button.interactable = true;
    }

    private void OnClick()
    {
        _button.interactable = false;
        _deckManager?.ResetDeck();
    }
}