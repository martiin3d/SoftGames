using UnityEngine;

/// <summary>
/// Bootstrap for the “Ace of Shadows” game.
/// This component is responsible for wiring up the core UI elements when the scene starts:
/// • The DeckManager, which handles card logic.
/// • The replay button that allows the player to view replays.
/// • The Game‑Complete view that appears when the player finishes the level.
/// </summary>
public class AceOfShadowsBootstrap : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private ReplayButtonView _replayButton;
    [SerializeField] private AceOfShadowsGameCompleteView _completeView;

    private void Awake()
    {
        _replayButton.Initialize(_deckManager);
        _completeView.Initialize(_deckManager);
    }
}