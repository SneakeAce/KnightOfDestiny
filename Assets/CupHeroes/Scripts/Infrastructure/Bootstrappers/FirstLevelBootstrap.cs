public class FirstLevelBootstrap
{
    private EnemyWaveController _waveController;
    private IEnemyPoolManager _poolManager;

    private CamerasController _camerasController;
    
    private CharacterSpawner _characterSpawner;
    private PlayerHUD _playerHUD;
    private ICurrencyController _currencyController;

    public FirstLevelBootstrap(
        CamerasController camerasController, PlayerHUD playerHUD, ICurrencyController currencyController, 
        CharacterSpawner characterSpawner)
    {
        //_waveController = waveController;
        //_poolManager = poolManager;
        _camerasController = camerasController;
        _playerHUD = playerHUD;

        _currencyController = currencyController;
        _characterSpawner = characterSpawner;

        Initialize();
    }

    private void Initialize()
    {
        Character character = _characterSpawner.CreateCharacter();

        _camerasController.Initialize();
        _camerasController.SetTargetForCamera(character);

        _playerHUD.Initialize();

        _currencyController.Initialize();

        //_poolManager.Initialize(); 

        //StartWaveController();
    }

    private void StartWaveController()
    {
        //_waveController.Initialize();
        //
        //_waveController.StartWave();

    }
}
