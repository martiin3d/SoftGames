using UnityEngine;

/// <summary>
/// Represents a single card in the game.
/// Implements <see cref="ICard"/> and provides methods for positioning the card within a stack
/// and resetting it to its default state.
/// </summary>
public class Card : MonoBehaviour, ICard
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Transform Transform => transform;

    /// <summary>
    /// Positions the card within a stack by offsetting its local Y position and setting the sorting order so that cards further back are rendered behind.
    /// </summary>
    /// <param name="indexInStack">
    /// Zero‑based index of this card in the stack (0 = top).
    /// </param>
    /// <param name="yOverlap">
    /// Amount of vertical overlap between adjacent cards.  
    /// The Y position will be set to -yOverlap * indexInStack.
    /// </param>
    public void SetCardPosition(int indexInStack, float yOverlap)
    {
        Vector3 localPosition = transform.localPosition;
        localPosition.y = -yOverlap * indexInStack;

        transform.localPosition = localPosition;
        _spriteRenderer.sortingOrder = indexInStack;
    }

    /// <summary>
    /// Resets the card to its default orientation and position within a stack.
    /// The rotation is cleared, Y position is calculated based on the stack index, and the sorting order is updated accordingly.
    /// </summary>
    /// <param name="indexInStack">
    /// Zero‑based index of this card in the stack (0 = top).
    /// </param>
    /// <param name="yOverlap">
    /// Amount of vertical overlap between adjacent cards.  
    /// The Y position will be set to -yOverlap * indexInStack.
    /// </param>
    public void ResetCardPosition(int indexInStack, float yOverlap)
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0, -yOverlap * indexInStack, 0);
        _spriteRenderer.sortingOrder = indexInStack;
    }
}