using UnityEngine;
using Zenject;

public class EnemySpawner
{
    private IEnemyFactory _enemyFactory;
    private IEntityBuilder _entityBuilder;

    private IEnemyControllersFactory _enemyControllersFactory;

    public EnemySpawner(IEnemyFactory enemyFactory, IEntityBuilder entityBuilder,
        IEnemyControllersFactory enemyControllersFactory)
    {
        _enemyFactory = enemyFactory;
        _entityBuilder = entityBuilder;
        _enemyControllersFactory = enemyControllersFactory;
    }

    public IEnemy SpawnEnemy(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        IEntity enemy = _enemyFactory.CreateEnemy();

        if (enemy == null)
            return null;

        enemy.Transform.position = spawnPosition;
        enemy.Transform.rotation = spawnRotation;

        _entityBuilder.BuildEntity(ref enemy);

        CreateEnemyController((IEnemy)enemy);

        return (IEnemy)enemy;
    }

    private void CreateEnemyController(IEnemy enemy)
    {
        EnemyController controller = _enemyControllersFactory.CreateEnemyController(enemy);

        if (controller == null)
        {
            Debug.Log("EnemySpawner. enemyController = null!");
            return;
        }

        enemy.SetController(controller);
    }

}
