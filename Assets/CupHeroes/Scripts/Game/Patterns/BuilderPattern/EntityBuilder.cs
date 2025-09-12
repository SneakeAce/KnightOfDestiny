using System;
using UnityEngine;

public class EntityBuilder : IEntityBuilder
{
    private const float OffsetPositionHealthBarByY = -0.07f;
    private const float MinValueByAxes = 0f;

    private const UIElementType HealthBarType = UIElementType.HealthBar; 

    private IUISpawner _uiSpawner;

    public EntityBuilder(IUISpawner uiSpawner)
    {
        _uiSpawner = uiSpawner;
    }

    public IEntity BuildEntity(ref IEntity baseEntity)
    {
        HealthBarView bar = GetHealthBar(baseEntity);

        CreatePopupController(baseEntity, bar.transform);

        return baseEntity;
    }

    private HealthBarView GetHealthBar(IEntity entity)
    {
        UISpawnerData data = new UISpawnerData(HealthBarType, Vector2.zero, Quaternion.identity);

        HealthBarView view = (HealthBarView)_uiSpawner.SpawnObject(data);

        if (view == null)
            throw new ArgumentNullException("healthBar in EntityBuilder is null!");

        HealthBarController barController = new HealthBarController(entity, view);

        barController.Initialize();

        view.transform.SetParent(entity.Transform);

        float heightEntity = entity.Collider.bounds.size.y;

        view.transform.position = new Vector2(
            entity.Transform.position.x, 
            entity.Transform.position.y + OffsetPositionHealthBarByY + heightEntity
            );

        view.transform.eulerAngles = new Vector3(MinValueByAxes, entity.Transform.rotation.y, MinValueByAxes);

        return view;
    }

    private void CreatePopupController(IEntity entity, Transform transform)
    {
        PopupController popupController = new PopupController(entity, _uiSpawner, transform);
        popupController.Initialize();
    }
}
