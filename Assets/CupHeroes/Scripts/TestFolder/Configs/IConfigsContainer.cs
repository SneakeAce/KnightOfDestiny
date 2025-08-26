using System.Collections.Generic;

public interface IConfigsContainer
{
    List<LibraryConfigsBase> LibraryConfigs { get; }
    List<SingleConfigBase> SingleConfigs { get; }
    List<PoolsConfigBase> PoolsConfigs { get; }
}