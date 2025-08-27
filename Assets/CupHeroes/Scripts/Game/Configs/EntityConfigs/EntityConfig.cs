using UnityEngine;

public abstract class EntityConfig : SingleConfigBase
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public EntityMainStats MainStats { get; private set; }
}
