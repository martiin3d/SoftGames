using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// Central controller that manages the life‑cycle of a deck of cards.
/// It creates an initial pool, randomly moves cards between stacks and provides a reset mechanism to return all cards to the source stack.
/// </summary>
public class DeckManager : MonoBehaviour , IDeckManager
{
    [SerializeField] private Card _cardPrefab;

    [SerializeField] private List<CardStack> _stacks;

    [SerializeField] private int _initialCardCount = 144;
    [SerializeField] private float _moveIntervalSeconds = 1f;
    [SerializeField] private float _moveDurationSeconds = 0.4f;


    private ICardMover _mover;
    private int _movesPlanned;

    public event Action OnDeckFinished;
    public event Action OnRestartFinished;
    private bool _isInResetMode = false;

    private void Awake()
    {
        _mover = new CardMover(0.5f);

        foreach (var stack in _stacks)
        {
            stack.Initialize();
        }
    }

    private void Start()
    {
        _isInResetMode = false;
        BuildInitialDeck();
        _movesPlanned = _initialCardCount;
        StartCoroutine(MoveCardsRoutine());
    }

    /// <summary>
    /// Instantiates the initial set of cards and places them in the source stack.
    /// </summary>
    private void BuildInitialDeck()
    {
        var source = _stacks[0];
        for (int i = 0; i < _initialCardCount; i++)
        {
            var card = Instantiate(_cardPrefab, source.Transform.position, Quaternion.identity);
            source.Push(card);
        }
        source.SetCardsPosition();
    }

    /// <summary>
    /// Coroutine that moves cards from the source stack to random destination stacks one by one until the planned number of moves is reached.
    /// </summary>
    private IEnumerator MoveCardsRoutine()
    {
        ICardStack sourceStack = _stacks[0];

        for (int i = 0; i < _movesPlanned; i++)
        {
            ICard card = sourceStack.PopTop();
            if (card == null) 
                break;

            int destinationStackIndex = Random.Range(1, _stacks.Count);
            ICardStack destinationStack = _stacks[destinationStackIndex];

            yield return StartCoroutine(_mover.Move(card, destinationStack, _moveDurationSeconds));

            destinationStack.Push(card);
            destinationStack.SetCardsPosition();

            yield return new WaitForSeconds(_moveIntervalSeconds);
        }

        OnDeckFinished?.Invoke();
    }

    /// <summary>
    /// Moves all cards back to the source stack and resets every stack's layout.
    /// </summary>
    public void ResetDeck()
    {
        if (_isInResetMode)
            return;

        _isInResetMode = true;

        StopAllCoroutines();

        ICardStack sourceStack = _stacks[0];

        foreach (ICardStack stack in _stacks)
        {
            if (stack == sourceStack) 
                continue;

            while (stack.Count > 0)
            {
                var card = stack.PopTop();
                sourceStack.Push(card);
            }
        }

        foreach (var stack in _stacks)
        {
            stack.ResetCardsPosition();
            stack.UpdateTextCounter();
        }

        StartCoroutine(MoveCardsRoutine());

        OnRestartFinished?.Invoke();

        _isInResetMode = false;
    }
}
