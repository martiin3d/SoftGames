using UnityEngine;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Represents a stack of cards in the scene.
/// Implements <see cref="ICardStack"/> and provides methods for manipulating the collection (push, pop, positioning) as well as keeping a UI counter updated.
/// </summary>
public class CardStack : MonoBehaviour, ICardStack
{
    /// <inheritdoc cref="ICardStack.Transform"/>
    public Transform Transform => transform;

    /// <inheritdoc cref="ICardStack.Count"/>
    public int Count => _cards.Count;

    [SerializeField] private TMP_Text _counterLabel;
    [SerializeField] private float _yOverlap = 0.03f;

    private List<ICard> _cards;

    /// <summary>
    /// Initializes internal structures and updates the UI counter.
    /// </summary>
    public void Initialize()
    {
        _cards = new List<ICard>();
        UpdateTextCounter();
    }

    /// <inheritdoc cref="ICardStack.Push"/>
    public void Push(ICard card)
    {
        card.Transform.SetParent(transform, true);
        _cards.Add(card);
        card.SetCardPosition(_cards.Count - 1, _yOverlap);

        UpdateTextCounter();
    }

    /// <inheritdoc cref="ICardStack.PopTop"/>
    public ICard PopTop()
    {
        if (_cards.Count == 0)
            return null;

        int last = _cards.Count - 1;
        ICard top = _cards[last];

        _cards.RemoveAt(last);
        UpdateTextCounter();

        return top;
    }

    /// <inheritdoc cref="ICardStack.GetTopPosition"/>
    public Vector3 GetTopPosition()
    {
        int latestCard = Mathf.Max(0, _cards.Count - 1);
        return transform.TransformPoint(new Vector3(0f, -_yOverlap * latestCard, 0f));
    }

    /// <summary>
    /// Re-applies the visual positions for all cards in the stack.
    /// </summary>
    public void SetCardsPosition()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetCardPosition(i, _yOverlap);
        }
    }

    /// <summary>
    /// Resets each card's transform to its default state within the stack.
    /// </summary>
    public void ResetCardsPosition()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].ResetCardPosition(i, _yOverlap);
        }
    }

    /// <summary>
    /// Updates the UI counter to match the current number of cards.
    /// </summary>
    public void UpdateTextCounter()
    {
        _counterLabel.text = Count.ToString();
    }
}