using System;

/// <summary>
/// Defines the responsibilities of a dialogue system that can start a sequence asynchronously.
/// </summary>
public interface IDialogueManager
{
    /// <summary>
    /// Initiates the dialogue flow.
    /// </summary>
    /// <param name="onDialogueComplete">
    /// Callback invoked when the entire dialogue sequence finishes successfully.
    /// </param>
    /// <param name="onFail">
    /// Callback invoked if the dialogue cannot be started or fails during execution.
    /// </param>
    void StartDialogueSequenceAsync(Action onDialogueComplete, Action onFail);
}