using System;
using UnityEngine;

public class EntityBuilder : IEntityBuilder
{
    private const float OffsetPositionHealthBarByY = 2f;

    private IUISpawner _uiSpawner;
    //private IConfigsProivder _configsProvider;

    public EntityBuilder(IUISpawner uiSpawner)
    {
        _uiSpawner = uiSpawner;
    }

    public IEntity BuildEntity(IEntity baseEntity)
    {
        IEntity entityInstance = baseEntity;

        HealthBarView bar = GetHealthBar(entityInstance);

        bar.transform.SetParent(entityInstance.Transform);
        bar.transform.position = new Vector2(entityInstance.Transform.position.x, entityInstance.Transform.position.y + OffsetPositionHealthBarByY);

        var result = entityInstance;

        return result;
    }

    private HealthBarView GetHealthBar(IEntity entity)
    {
        UISPawnerData data = new UISPawnerData();

        HealthBarView view = _uiSpawner.SpawnObject<HealthBarView>(data);

        if (view == null)
            throw new ArgumentNullException("healthBar in EntityBuilder is null!");

        HealthBarController barController = new HealthBarController(entity, view);
        barController.Initialize();

        return view;
    }

}
