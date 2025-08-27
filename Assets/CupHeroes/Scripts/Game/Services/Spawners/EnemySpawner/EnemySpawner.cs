using UnityEngine;

public class EnemySpawner
{
    private IEnemyFactory _enemyFactory;
    private IEntityBuilder _entityBuilder;

    public EnemySpawner(IEnemyFactory enemyFactory, IEntityBuilder entityBuilder)
    {
        _enemyFactory = enemyFactory;
        _entityBuilder = entityBuilder;
    }

    public IEnemy SpawnEnemy(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        IEntity enemy = _enemyFactory.CreateEnemy();

        if (enemy == null)
            return null;

        enemy.Transform.position = spawnPosition;
        enemy.Transform.rotation = spawnRotation;

        _entityBuilder.BuildEntity(ref enemy);

        return (IEnemy)enemy;
    }
}
