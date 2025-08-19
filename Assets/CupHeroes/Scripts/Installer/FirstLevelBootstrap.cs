public class FirstLevelBootstrap
{
    //Test
    private EnemyWaveController _waveController;
    private IEnemyPoolManager _poolManager;

    public FirstLevelBootstrap(EnemyWaveController waveController, IEnemyPoolManager poolManager)
    {
        _waveController = waveController;
        _poolManager = poolManager;

        Initialize();
    }

    private void Initialize()
    {
        _poolManager.Initialize(); 

        StartWaveController();
    }

    private void StartWaveController()
    {
        _waveController.Initialize();

        _waveController.StartWave();
    }
}
