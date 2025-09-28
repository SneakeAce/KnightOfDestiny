using UnityEngine;

public interface ICharacter : IEntity
{
    public Transform ProjectileSpawnPosition { get; }

}
