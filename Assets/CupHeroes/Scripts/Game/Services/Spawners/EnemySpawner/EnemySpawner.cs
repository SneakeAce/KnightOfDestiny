using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner
{
    private IEnemyFactory _enemyFactory;
    private IEntityBuilder _entityBuilder;

    private IInstantiator _container;

    public EnemySpawner(IEnemyFactory enemyFactory, IEntityBuilder entityBuilder, IInstantiator container)
    {
        _enemyFactory = enemyFactory;
        _entityBuilder = entityBuilder;
        _container = container;
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
        EnemyController controller = _container.Instantiate<EnemyController>(new object[] { enemy });
        controller?.Initialize();

        enemy?.SetController(controller);
    }
}
