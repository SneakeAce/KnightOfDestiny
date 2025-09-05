using System;
using System.Collections;
using UnityEngine;

public class AttackState : IEntityState, IDisposable
{
    private IEntity _entity;
    private IEntity _target;

    private CoroutinePerformer _performer;
    private Coroutine _attackCoroutine;

    private float _damage;
    private float _delayBetweenAttack;
    private float _attackRange;
    private bool _canAttack = false;

    public AttackState(IEntity entity, IEntity target, CoroutinePerformer performer)
    {
        _entity = entity;
        _target = target;
        _performer = performer;
    }

    public void Dispose()
    {
        _target.Health.EntityDied -= OnTargetDestroyed;
    }

    public void Enter()
    {
        SetParameters();

        _target.Health.EntityDied += OnTargetDestroyed;

        _canAttack = true;
        _attackCoroutine = _performer.StartCoroutine(AttackJob());
    }

    public void Exit()
    {
        _target.Health.EntityDied -= OnTargetDestroyed;

        if (_attackCoroutine != null)
        {
            _performer.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _canAttack = false;
    }

    public void Update()
    {
        return;
    }

    private void SetParameters()
    {
        _damage = _entity.Config.AttackStats.BaseDamage;
        _delayBetweenAttack = _entity.Config.AttackStats.BaseAttackSpeed;
        _attackRange = _entity.Config.AttackStats.AttackRange;
    }

    private IEnumerator AttackJob()
    {
        while (_canAttack && _target != null)
        {
            if (CheckDistanceToTarget() == false)
                yield break;

            DamageData data = new DamageData(_damage);

            _target.Health.TakeDamage(data);

            yield return new WaitForSeconds(_delayBetweenAttack);
        }
    }

    private bool CheckDistanceToTarget()
    {
        float sqrDistance = (_entity.Transform.position - _target.Transform.position).sqrMagnitude;

        float sqrAttackRange = _attackRange * _attackRange;

        if (sqrDistance <= sqrAttackRange)
            return true;

        return false;
    }

    private void OnTargetDestroyed(IEntity entity)
    {
        _target = null;
    }
}
