using UnityEngine;

public interface IUISpawner 
{
    T SpawnObject<T>(UISPawnerData spawnerData) where T : MonoBehaviour;
}
