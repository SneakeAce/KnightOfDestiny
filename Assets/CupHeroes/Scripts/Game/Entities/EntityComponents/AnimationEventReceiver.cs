using System;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour, IAnimationEventReceiver
{
    public event Action OnFrameAttack;

    public void AttackFrame()
    {
        OnFrameAttack?.Invoke();
    }
}
