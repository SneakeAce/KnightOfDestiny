using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : IDisposable
{
    private IEnemyFactory _enemyFactory;
    private IEntityBuilder _entityBuilder;

    private CollectingCurrencyHandler _currencyHandler;

    private List<IEnemyHealth> _enemiesHealthList = new();

    public EnemySpawner(IEnemyFactory enemyFactory, IEntityBuilder entityBuilder, CollectingCurrencyHandler currencyHandler)
    {
        _enemyFactory = enemyFactory;
        _entityBuilder = entityBuilder;
        _currencyHandler = currencyHandler;
    }

    public void Dispose()
    {
        if (_enemiesHealthList.Count == 0)
            return;

        foreach (IEnemyHealth enemyHealth in _enemiesHealthList)
        {
            enemyHealth.OnGiveAwayCurrency -= _currencyHandler.GetCurrency;
        }
    }

    public IEnemy SpawnEnemy(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        IEntity enemy = _enemyFactory.CreateEnemy();

        if (enemy == null)
            return null;

        enemy.Transform.position = spawnPosition;
        enemy.Transform.rotation = spawnRotation;

        _entityBuilder.BuildEntity(ref enemy);

        SubscribingCurrencyHandler(enemy);

        return (IEnemy)enemy;
    }

    private void SubscribingCurrencyHandler(IEntity enemy) 
    {
        IEnemyHealth enemyHealth = enemy.Health as IEnemyHealth;

        if (enemyHealth == null)
            throw new InvalidCastException("Invalid cast to IEnemyHealth in EnemySpawner");

        _enemiesHealthList.Add(enemyHealth);

        enemyHealth.OnGiveAwayCurrency += _currencyHandler.GetCurrency;
    }
}
