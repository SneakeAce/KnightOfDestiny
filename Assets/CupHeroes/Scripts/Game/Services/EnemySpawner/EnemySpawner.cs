using UnityEngine;

public class EnemySpawner
{
    private IEnemyFactory _enemyFactory;
    private IEntityBuilder _entityBuilder;

    public EnemySpawner(IEnemyFactory enemyFactory, IEntityBuilder entityBuilder)
    {
        _enemyFactory = enemyFactory;
    }

    public IEnemy SpawnEnemy(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        IEnemy enemy = _enemyFactory.CreateEnemy();

        if (enemy == null)
            return null;

        enemy.Transform.position = spawnPosition;
        enemy.Transform.rotation = spawnRotation;

        return enemy;
    }
}
