using System;
using UnityEngine;

public class FireAnimationManager : MonoBehaviour
{
    private Action _onAnimationEnd;

    public void Initialize(Action onAnimationEnd)
    {
        _onAnimationEnd = onAnimationEnd;
    }

    public void OnAnimationEnd()
    {
        _onAnimationEnd?.Invoke();
    }
}
