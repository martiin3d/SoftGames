using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the animated movement of a card from its current location to the top position of a destination stack.
/// Implements <see cref="ICardMover"/> and uses a simple easing function combined with a sinusoidal "peak" effect for a smooth, natural motion.
/// </summary>
public class CardMover : ICardMover
{
    private readonly float peakHeight;

    /// <summary>
    /// Creates a new <see cref="CardMover"/> with an optional peak height.
    /// </summary>
    /// <param name="peak">Peak height for the card's trajectory. Default is 0.5.</param>
    public CardMover(float peak = 0.5f)
    {
        peakHeight = peak;
    }

    /// <inheritdoc cref="ICardMover.Move"/>
    /// <remarks>
    /// The coroutine interpolates from <paramref name="card"/>'s current position to the top position of <paramref name="dest"/> over <paramref name="duration"/>.
    /// During the animation, a sinusoidal bump is added along the Y axis and the card receives a small random rotation that eases in/out with the movement.
    /// </remarks>
    public IEnumerator Move(ICard card, ICardStack dest, float duration)
    {
        Vector3 start = card.Transform.position;
        Vector3 end = dest.GetTopPosition();

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime / Mathf.Max(0.0001f, duration);
            float ease = Ease(time);

            // Linear interpolation between start and end
            Vector3 pos = Vector3.Lerp(start, end, ease);

            // Sinusoidal peak in the Y axis
            pos.y += Mathf.Sin(ease * Mathf.PI) * peakHeight;

            card.Transform.position = pos;
            card.Transform.rotation = GetRandomRotation(ease);

            yield return null;
        }
        card.Transform.position = end;
    }

    /// <summary>
    /// Returns a small random rotation around the Z axis that eases in/out with the movement progress. The rotation range is +-10°.
    /// </summary>
    private Quaternion GetRandomRotation(float ease)
    {
        return Quaternion.Euler(0, 0, Mathf.Lerp(0f, Random.Range(-10f, 10f), ease));
    }

    /// <summary>
    /// Cubic easing function (smooth start and end). Clamps input to [0,1].
    /// </summary>
    private static float Ease(float x)
    {
        x = Mathf.Clamp01(x);
        return x * x * (3f - 2f * x);
    }
}