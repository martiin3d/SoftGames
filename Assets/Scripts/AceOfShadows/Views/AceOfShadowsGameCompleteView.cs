using System;
using UnityEngine;

public class AceOfShadowsGameCompleteView : MonoBehaviour
{
    private IDeckManager _deckManager;

    public void Initialize(IDeckManager deckManager)
    {
        _deckManager = deckManager;
        _deckManager.OnDeckFinished += ShowPanel;
        _deckManager.OnRestartFinished += OnRestartFinished;
    }

    private void OnRestartFinished()
    {
        gameObject.SetActive(false);
    }

    private void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (_deckManager != null)
        {
            _deckManager.OnDeckFinished -= ShowPanel;
        }
    }
}
