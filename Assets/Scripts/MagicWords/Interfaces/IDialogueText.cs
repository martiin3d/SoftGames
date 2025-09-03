using System.Collections;

/// <summary>
/// Represents a component that can display dialogue text over time.
/// </summary>
public interface IDialogueText
{
    /// <summary>
    /// Creates a coroutine that shows the provided <paramref name="dialog"/> char by char, handling any timing, animations or user input required to progress.
    /// </summary>
    /// <param name="dialog">The dialogue data containing text, speaker, etc.</param>
    /// <returns>A coroutine that yields until the dialogue has finished being displayed.</returns>
    IEnumerator ShowDialogueCoroutine(DialogData dialog);
}