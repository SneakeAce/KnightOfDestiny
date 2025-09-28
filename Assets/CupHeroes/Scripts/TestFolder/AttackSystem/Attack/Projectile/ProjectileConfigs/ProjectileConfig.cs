using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ProjectileConfig/Projectile", fileName = "ProjectileConfig")]
public class ProjectileConfig : SingleConfigBase
{
    [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
    [field: SerializeField] public ProjectileStats MainStats { get; private set; }

    public override T GetConfig<T>()
    {
        return this as T;
    }
}
