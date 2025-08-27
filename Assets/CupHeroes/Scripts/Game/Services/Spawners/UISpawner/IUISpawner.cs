using UnityEngine;

public interface IUISpawner 
{
    T SpawnObject<T>(UISpawnerData spawnerData) where T : MonoBehaviour;
}
