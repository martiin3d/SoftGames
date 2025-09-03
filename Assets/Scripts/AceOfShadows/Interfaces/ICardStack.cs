using UnityEngine;

/// <summary>
/// Represents a vertical stack of cards in the game.
/// A stack manages its own transform, keeps track of contained cards, and provides helper methods for positioning and counting them.
/// </summary>
public interface ICardStack
{
    /// <summary>
    /// Number of cards currently in this stack.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// The Unity <see cref="Transform"/> that represents the root position of the stack.
    /// </summary>
    Transform Transform { get; }

    /// <summary>
    /// Prepares the stack for use.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Returns the world position where a new card should be placed on top of the stack.
    /// </summary>
    /// <returns>World space coordinate for the topmost card.</returns>
    Vector3 GetTopPosition();

    /// <summary>
    /// Removes and returns the card at the top of the stack.  
    /// If the stack is empty, null is returned.
    /// </summary>
    /// <returns>The top <see cref="ICard"/> or null if the stack has no cards.</returns>
    ICard PopTop();

    /// <summary>
    /// Adds a card to the top of the stack and updates its local position accordingly.
    /// Implementations may also trigger visual effects such as a slight lift animation.
    /// </summary>
    /// <param name="card">The card to push onto this stack.</param>
    void Push(ICard card);

    /// <summary>
    /// Re-applies the visual positions for all cards in the stack.
    /// </summary>
    void SetCardsPosition();

    /// <summary>
    /// Updates the UI counter to match the current number of cards.
    /// </summary>
    void UpdateTextCounter();

    /// <summary>
    /// Resets all card positions to their initial layout.
    /// </summary>
    void ResetCardsPosition();
}