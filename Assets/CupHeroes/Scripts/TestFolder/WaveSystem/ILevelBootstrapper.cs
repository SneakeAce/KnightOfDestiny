using System;

public interface ILevelBootstrapper
{
    event Action OnInitialized;
    void Initialize();
}
