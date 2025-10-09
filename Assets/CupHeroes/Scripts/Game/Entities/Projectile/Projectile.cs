using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    private ProjectileMoveController _moveComponent; 

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private ProjectileConfig _projectileConfig;
    private ProjectileController _controller;

    private IObjectPool _currentPool;
    private IEntity _parent;

    public Transform Transform => transform;
    public Collider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public ProjectileConfig ProjectileConfig => _projectileConfig;
    public ProjectileMoveController MoveComponent => _moveComponent;
    public ProjectileController Controller => _controller;
    public IEntity Parent => _parent;

    public void Initialize()
    {
        SetComponents();
    }

    public void SetParent(IEntity parent) => _parent = parent;

    public void SetConfig(ProjectileConfig config) => _projectileConfig = config;
    
    public void SetPool(IObjectPool currentPool) => _currentPool = currentPool;

    private void ReturnInPool(IProjectile projectile) => _currentPool.ReturnPoolObject(projectile as Projectile);

    public void SetController(ProjectileController controller)
    {
        _controller = controller;

        _controller.ProjectileDestroyed += ReturnInPool;
    }
       
    private void SetComponents()
    {
        if (_collider == null)
            _collider = GetComponent<Collider2D>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    private void UnsubcribingEvents()
    {
        if (_controller != null)
            _controller.ProjectileDestroyed -= ReturnInPool;
    }

    private void OnDisable()
    {
        UnsubcribingEvents();
    }
}
