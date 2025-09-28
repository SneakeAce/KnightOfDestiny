using System;
using System.Collections;
using UnityEngine;

public class ProjectileMoveController : IDisposable
{
    private IProjectile _projectile;

    private CoroutinePerformer _performer;
    private Coroutine _moveCoroutine;

    private Vector2 _startPosition;

    private float _distanceFlying;

    public ProjectileMoveController(IProjectile projectile, CoroutinePerformer performer)
    {
        _projectile = projectile;
        _performer = performer;
    }

    public event Action<IProjectile> OnEndPoint;

    public void Dispose()
    {
        StopMoveJob();
        _projectile = null;

    }

    public void Initialize()
    {
        SetParameters();

        StopMoveJob();

        _moveCoroutine = _performer.StartCoroutine(MoveJob());
    }

    private void SetParameters()
    {
        _startPosition = _projectile.Transform.position;
        _distanceFlying = _projectile.ProjectileConfig.MainStats.DistanceFlying; // Брать дистанцию полета из статов персонажа.
    }

    private IEnumerator MoveJob()
    {
        Vector2 direction = GetDirection();

        _projectile.Rigidbody.linearVelocity = direction * _projectile.ProjectileConfig.MainStats.Speed;

        while (_projectile != null && CanMoveOn())
            yield return Yielders.FixedUpdate;
        
        OnEndPoint?.Invoke(_projectile);
    }

    private Vector2 GetDirection()
    {
        float yRot = _projectile.Transform.eulerAngles.y % 360f;

        if (Mathf.Abs(yRot - 180f) < 0.1f)
            return Vector2.left;

        return Vector2.right;
    }

    private bool CanMoveOn()
    {
        float sqrDistance = _distanceFlying * _distanceFlying;
        float sqrDistanceBetweenPoint = (_startPosition - (Vector2)_projectile.Transform.position).sqrMagnitude;

        if (sqrDistanceBetweenPoint >= sqrDistance)
            return false;

        return true;
    }

    private void StopMoveJob()
    {
        if (_moveCoroutine != null)
        {
            _performer.StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }
}
