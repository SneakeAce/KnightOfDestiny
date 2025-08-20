public class FirstLevelBootstrap
{
    private EnemyWaveController _waveController;
    private IEnemyPoolManager _poolManager;

    private CameraController _cameraController;
    private PlayerHUD _playerHUD;
    private ICurrencyController _currencyController;

    private HealthBarController _healthBarController;

    public FirstLevelBootstrap(EnemyWaveController waveController, IEnemyPoolManager poolManager, 
        CameraController cameraController, PlayerHUD playerHUD, ICurrencyController currencyController, HealthBarController healthBarController)
    {
        _waveController = waveController;
        _poolManager = poolManager;
        _cameraController = cameraController;
        _playerHUD = playerHUD;

        _currencyController = currencyController;
        _healthBarController = healthBarController;

        Initialize();
    }

    private void Initialize()
    {
        _cameraController.Initialize();

        _playerHUD.Initialize();

        _currencyController.Initialize();

        _poolManager.Initialize(); 

        _healthBarController.Initialize();

        StartWaveController();
    }

    private void StartWaveController()
    {
        _waveController.Initialize();

        _waveController.StartWave();

    }
}
