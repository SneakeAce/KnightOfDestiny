public class FirstLevelBootstrap
{
    //Test
    private EnemyWaveController _waveController;
    private IEnemyPoolManager _poolManager;

    private CameraController _cameraController;
    private PlayerHUD _playerHUD;
    private ICurrencyController _currencyController;

    CollectingCurrencyHandler _currencyHandler;

    public FirstLevelBootstrap(EnemyWaveController waveController, IEnemyPoolManager poolManager, 
        CameraController cameraController, PlayerHUD playerHUD, CollectingCurrencyHandler currencyHandler,
        ICurrencyController currencyController)
    {
        _waveController = waveController;
        _poolManager = poolManager;
        _cameraController = cameraController;
        _playerHUD = playerHUD;

        _currencyHandler = currencyHandler;
        _currencyController = currencyController;

        Initialize();
    }

    private void Initialize()
    {
        _cameraController.Initialize();

        _playerHUD.Initialize();

        _currencyController.Initialize();

        _poolManager.Initialize(); 

        StartWaveController();
    }

    private void StartWaveController()
    {
        _waveController.Initialize();

        _waveController.StartWave();

        int cur = 10;

        _currencyHandler.GetCurrency(cur);
    }
}
