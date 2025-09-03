using System;

/// <summary>
/// Central manager that coordinates the state of all card stacks and exposes events for key lifecycle milestones.
/// </summary>
public interface IDeckManager
{
    /// <summary>
    /// Invoked after the deck has been reset to its initial configuration.
    /// </summary>
    public event Action OnRestartFinished;

    /// <summary>
    /// Invoked when all planned card movements have completed and the deck is in a finished state.
    /// </summary>
    public event Action OnDeckFinished;

    /// <summary>
    /// Restores the entire deck to its starting layout, moving every card back to the source stack and resetting all visual counters.
    /// </summary>
    void ResetDeck();
}