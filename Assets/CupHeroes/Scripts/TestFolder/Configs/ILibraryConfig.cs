using System.Collections.Generic;
using UnityEngine;

public interface ILibraryConfig
{
     List<T> GetConfigs<T>() where T : ScriptableObject;
}
