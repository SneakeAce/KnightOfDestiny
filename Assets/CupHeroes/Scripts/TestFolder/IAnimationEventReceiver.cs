using System;

public interface IAnimationEventReceiver
{
    event Action OnFrameAttack;

    void AttackFrame();
}
