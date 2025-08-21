using UnityEngine;

public class EnemySpawner
{
    private IEnemyFactory _enemyFactory;

    public EnemySpawner(IEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }

    public Enemy SpawnEnemy(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        Enemy enemy = _enemyFactory.CreateEnemy();

        if (enemy == null)
            return null;

        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = spawnRotation;

        return enemy;
    }
}
