using System.Collections.Generic;
using UnityEngine;

public abstract class LibraryConfigsBase : ScriptableObject
{
    abstract public List<T> GetConfigs<T>() where T : ScriptableObject;
}
