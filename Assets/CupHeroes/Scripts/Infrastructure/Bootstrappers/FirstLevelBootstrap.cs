public class FirstLevelBootstrap
{
    private EnemyWaveController _waveController;
    private IPoolsManager _poolsManager;

    private CamerasController _camerasController;
    
    private CharacterSpawner _characterSpawner;
    private PlayerHUD _playerHUD;
    private ICurrencyController _currencyController;
    private CurrencyDisplayController _currencyDisplayController;

    public FirstLevelBootstrap(IPoolsManager poolsManager, EnemyWaveController waveController,
        CamerasController camerasController, PlayerHUD playerHUD, 
        ICurrencyController currencyController, CurrencyDisplayController currencyDisplayController,
        CharacterSpawner characterSpawner)
    {
        _waveController = waveController;
        _poolsManager = poolsManager;
        _camerasController = camerasController;
        _playerHUD = playerHUD;

        _currencyController = currencyController;
        _currencyDisplayController = currencyDisplayController;

        _characterSpawner = characterSpawner;

        Initialize();
    }

    private void Initialize()
    {
        _poolsManager.Initialize();

        Character character = _characterSpawner.CreateCharacter();

        _camerasController.Initialize();
        _camerasController.SetTargetForCamera(character);

        _playerHUD.Initialize();

        _currencyController.Initialize();
        _currencyDisplayController.Initialize();

        StartWaveController();
    }

    private void StartWaveController()
    {
        _waveController.Initialize();
        
        _waveController.StartWave();
    }
}
