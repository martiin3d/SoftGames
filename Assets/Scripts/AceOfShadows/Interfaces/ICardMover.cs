using System.Collections;

/// <summary>
/// Defines a strategy for animating a card from its current position to a target stack.
/// </summary>
public interface ICardMover
{
    /// <summary>
    /// Moves the specified <paramref name="card"/> into the destination stack <paramref name="dest"/> over the time span defined by <paramref name="duration"/>.
    /// </summary>
    /// <param name="card">The card to move.</param>
    /// <param name="dest">The destination stack where the card will be placed.</param>
    /// <param name="duration">
    /// Total time in seconds that the movement should take.
    /// </param>
    /// <returns>A coroutine that performs the move operation.</returns>
    IEnumerator Move(ICard card, ICardStack dest, float duration);
}