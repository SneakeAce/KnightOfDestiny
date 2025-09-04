using System;
using UnityEngine;

public class PopupController : IDisposable
{
    private const UIElementType _damagePopupType = UIElementType.DamagePopup;

    private IUISpawner _uiSpawner;

    private IEntity _entity;

    private Transform _spawnPosition;

    public PopupController(IEntity entity, IUISpawner uiSpawner, Transform spawnPosition)
    {
        _entity = entity;
        _uiSpawner = uiSpawner;
        _spawnPosition = spawnPosition;
    }

    public void Dispose()
    {
        _entity.Health.OnTakingDamage -= CreateDamagePopup;
    }

    public void Initialize()
    {
        _entity.Health.OnTakingDamage += CreateDamagePopup;
    }

    public void CreateDamagePopup(float currentValue)
    {
        if (_spawnPosition == null)
        {
            Debug.LogError("Entity in DamagePopupController is null!");
            return;
        }

        UISpawnerData data = new UISpawnerData(_damagePopupType, _spawnPosition.position, Quaternion.identity);

        DamagePopupView damagePopupViewIntstance = (DamagePopupView)_uiSpawner.SpawnObject(data);

        if (damagePopupViewIntstance == null)
        {
            Debug.Log("DamagePopupView in DamagePopupController is null!");
            return;
        }

        Transform parent = damagePopupViewIntstance.transform.parent;

        damagePopupViewIntstance.transform.SetParent(_spawnPosition);

        damagePopupViewIntstance.transform.localScale = Vector3.one;

        damagePopupViewIntstance.transform.position = _spawnPosition.position;
        damagePopupViewIntstance.transform.rotation = Quaternion.identity;

        damagePopupViewIntstance.ShowPopup(currentValue, parent);
    }

}
