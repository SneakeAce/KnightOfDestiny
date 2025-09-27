using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private ProjectileConfig _projectileConfig;
    private ProjectileMoveComponent _moveComponent;
    private ProjectileController _controller;

    private IObjectPool _currentPool;
    private IEntity _parent;

    public Transform Transform => transform;
    public Collider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public ProjectileConfig ProjectileConfig => _projectileConfig;
    public ProjectileMoveComponent MoveComponent => _moveComponent;
    public ProjectileController Controller => _controller;
    public IEntity Parent => _parent;

    public void Initialize()
    {
        SetComponents();

        InitializeComponents();
    }

    public void SetParent(IEntity parent) => _parent = parent;
   
    public void SetController(ProjectileController controller) => _controller = controller;
   
    public void SetConfig(ProjectileConfig config) => _projectileConfig = config;
    
    public void SetPool(IObjectPool currentPool) => _currentPool = currentPool;

    private void ReturnInPool(IProjectile projectile) => _currentPool.ReturnPoolObject(projectile as Projectile);
    
    private void SetComponents()
    {
        if (_collider == null)
            _collider = GetComponent<Collider2D>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    private void InitializeComponents()
    {
        if (_moveComponent == null)
        {
            _moveComponent = new ProjectileMoveComponent();

        }

    }

    private void OnDisable()
    {
        
    }
}
