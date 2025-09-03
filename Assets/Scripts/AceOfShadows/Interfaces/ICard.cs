using UnityEngine;

/// <summary>
/// Represents a single card in the game.
/// Implementations are responsible for exposing their Transform and handling position updates when cards are moved or rearranged within a stack.
/// </summary>
public interface ICard
{
    /// <summary>
    /// The Unity <see cref="Transform"/> component of the card.
    /// This allows the mover to manipulate its world position, rotation, etc.
    /// </summary>
    Transform Transform { get; }

    /// <summary>
    /// Resets the card's transform to the correct location for a given index within a stack. The method should keep the card in the same visual order but apply any required offset so that all cards are visible.
    /// </summary>
    /// <param name="indexInStack">Zero‑based position of this card in its stack.</param>
    /// <param name="yOverlap">Vertical offset applied between successive cards.</param>
    void ResetCardPosition(int indexInStack, float yOverlap);

    /// <summary>
    /// Sets the card's transform to a new location for a given index within a stack. Unlike <see cref="ResetCardPosition"/>, this method is intended to be called when cards are actively moved or rearranged.
    /// </summary>
    /// <param name="indexInStack">Zero‑based position of this card in its stack.</param>
    /// <param name="yOverlap">Vertical offset applied between successive cards.</param>
    void SetCardPosition(int indexInStack, float yOverlap);
}