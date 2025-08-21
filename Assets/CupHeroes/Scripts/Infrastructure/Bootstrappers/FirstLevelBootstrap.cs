public class FirstLevelBootstrap
{
    private EnemyWaveController _waveController;
    private IEnemyPoolManager _poolManager;

    private CameraController _cameraController;
    
    private CharacterSpawner _characterSpawner;
    private PlayerHUD _playerHUD;
    private ICurrencyController _currencyController;

    private HealthBarController _healthBarController;

    public FirstLevelBootstrap(EnemyWaveController waveController, IEnemyPoolManager poolManager, 
        CameraController cameraController, PlayerHUD playerHUD, ICurrencyController currencyController,
        HealthBarController healthBarController, CharacterSpawner characterSpawner)
    {
        _waveController = waveController;
        _poolManager = poolManager;
        _cameraController = cameraController;
        _playerHUD = playerHUD;

        _currencyController = currencyController;
        _healthBarController = healthBarController;
        _characterSpawner = characterSpawner;

        Initialize();
    }

    private void Initialize()
    {
        _characterSpawner.CreateCharacter();

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
