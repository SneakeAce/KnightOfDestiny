using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeAttackByOnceTarget : BaseAttackStrategy
{
    private List<IEntity> _targets = new();

    private IEntity _target;
    private ProjectileSpawner _projectileSpawner;

    public RangeAttackByOnceTarget(ProjectileSpawner projectileSpawner)
    {
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

        if (_target == entity)
            _target = null;

        _state.Entity.Animator.Play("Idle", 0, 0);

        _targets.Remove(entity);

        if (_targets.Count == 0)
        {
            Debug.Log($"{this.ToString()} OnEntityDestroyed - if all enemy died called Action");
            OnAllTargetsDestroyed?.Invoke();
        }
    }

    public override void SwitchTarget(IEntity newTarget)
    {
        Debug.Log($"{this.ToString()} SwitchTarget - newTarget = {newTarget}");

        if (_target != null)
            _target.Health.EntityDied -= OnEntityDestroyed;

        _target = newTarget;

        if (_target != null)
            _target.Health.EntityDied += OnEntityDestroyed;
    }

    public override void GetTargets(List<IEntity> targets)
    {
        _targets.Clear();

        _targets.AddRange(targets);

        Debug.Log($"{this.ToString()} GetTargets - _targets = {_targets.Count}");

        _target = targets.FirstOrDefault();

        if (_target != null)
            _target.Health.EntityDied += OnEntityDestroyed;
    }

    public override IEnumerator AttackJob()
    {
        while (_state.CanAttack)
        {
            _state.UpdateData();

            if (_target == null || CheckDistanceToTarget(_target) == false)
            {
                yield return null;
                continue;
            }

            if (_target != null)
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
