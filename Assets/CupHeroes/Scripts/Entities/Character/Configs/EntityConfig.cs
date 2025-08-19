using UnityEngine;

public class EntityConfig : ScriptableObject
{
    [field: SerializeField] public EntityType EntityType { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public EntityMainStats MainStats { get; private set; }
}
