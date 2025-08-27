public class GlobalBootstrapper
{
    private IConfigsProvider _configsProvider;

    public GlobalBootstrapper(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;

        Initialize();
    }

    private void Initialize()
    {
        _configsProvider.Initialize();
    }


}
