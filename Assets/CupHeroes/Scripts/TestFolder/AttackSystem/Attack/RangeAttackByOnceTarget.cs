using System;
using System.Collections;
using UnityEngine;

public class RangeAttackByOnceTarget : BaseAttackStrategy
{
    private IEntity _target;
    private ProjectileSpawner _projectileSpawner;

    public RangeAttackByOnceTarget(IEntity target, ProjectileSpawner projectileSpawner)
    {
        _target = target;
        _projectileSpawner = projectileSpawner;
    }

    public override event Action OnAllTargetsDestroyed;

    public override void SubscribingEvents()
    {
        if (_state.Entity != null)
        {
            _state.Entity.AnimationEventReceiver.OnFrameAttack += DealDamage;
            _state.Entity.Health.EntityDied += OnEntityDestroyed;
        }

        if (_target != null)
            _target.Health.EntityDied += OnEntityDestroyed;
    }

    public override void UnsubscribingEvents()
    {
        if (_state.Entity != null)
        {
            _state.Entity.AnimationEventReceiver.OnFrameAttack -= DealDamage;
            _state.Entity.Health.EntityDied -= OnEntityDestroyed;
        }

        if (_target != null)
            _target.Health.EntityDied -= OnEntityDestroyed;
    }

    public override void OnEntityDestroyed(IEntity entity)
    {
        entity.Health.EntityDied -= OnEntityDestroyed;

        OnAllTargetsDestroyed?.Invoke();
    }

    public override IEnumerator AttackJob()
    {
        while (_target != null && _state.CanAttack)
        {
            _state.UpdateData();

            if (CheckDistanceToTarget(_target) == false)
            {
                yield return null;
                continue;
            }

            if (_target == null)
                yield break;

            _state.Entity.Animator.SetTrigger("Attack");

            yield return new WaitForSeconds(_state.ClipDuration);

            if (_state.RemainingCooldown > 0f)
                yield return new WaitForSeconds(_state.RemainingCooldown);
        }
    }

    public override void DealDamage()
    {
        if (_state.Entity is not ICharacter character)
            throw new InvalidCastException($"{nameof(_state.Entity)} is not ICharacter!");

        ProjectileSpawnData data = new ProjectileSpawnData(
            character.ProjectileSpawnPosition.position,
            Quaternion.identity,
            character, 
            _target, 
            character.Config.AttackStats.AvailableProjectileType
            );

        IProjectile projectile = _projectileSpawner.SpawnProjectile(data);
    }


}
