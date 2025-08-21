using System;
using UnityEngine;

public class EntityBuilder : IEntityBuilder
{
    private const float OffsetPositionHealthBarByY = 2f;

    private IUISpawner _uiSpawner;
    private IConfigsProvider _configsProvider;

    private HealthBarConfig _healthBarConfig;

    public EntityBuilder(IUISpawner uiSpawner, IConfigsProvider configsProvider)
    {
        _uiSpawner = uiSpawner;
        _configsProvider = configsProvider;

        _healthBarConfig = _configsProvider.GetSingleConfig<HealthBarConfig>().GetConfig<HealthBarConfig>();
    }

    public IEntity BuildEntity(ref IEntity baseEntity)
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
        UISPawnerData data = new UISPawnerData(_healthBarConfig.Prefab, Vector2.zero, Quaternion.identity);

        HealthBarView view = _uiSpawner.SpawnObject<HealthBarView>(data);

        if (view == null)
            throw new ArgumentNullException("healthBar in EntityBuilder is null!");

        HealthBarController barController = new HealthBarController(entity, view);

        barController.Initialize();

        return view;
    }

}
