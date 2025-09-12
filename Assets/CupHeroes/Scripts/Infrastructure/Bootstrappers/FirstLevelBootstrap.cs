using System;

public class FirstLevelBootstrap : ILevelBootstrapper
{
    private EnemyWaveController _waveController;
    private IPoolsManager _poolsManager;

    private CamerasController _camerasController;
    
    private CharacterSpawner _characterSpawner;
    private PlayerHUD _playerHUD;
    private ICurrencyController _currencyController;
    private CurrencyDisplayController _currencyDisplayController;

    private TickUpdater _tickUpdater;
    private LevelController _levelManager;

    public FirstLevelBootstrap(IPoolsManager poolsManager, EnemyWaveController waveController,
        CamerasController camerasController, PlayerHUD playerHUD, 
        ICurrencyController currencyController, CurrencyDisplayController currencyDisplayController,
        CharacterSpawner characterSpawner, TickUpdater tickUpdater, LevelController levelManager)
    {
        _poolsManager = poolsManager;
        _waveController = waveController;
        _camerasController = camerasController;
        _playerHUD = playerHUD;

        _currencyController = currencyController;
        _currencyDisplayController = currencyDisplayController;

        _characterSpawner = characterSpawner;

        _tickUpdater = tickUpdater;

        _levelManager = levelManager;

        Initialize();
    }

    public event Action OnInitialized;

    public void Initialize()
    {
        InitializePoolsManager();

        Character character = CreateCharacter();

        InitializeCamerasController(character);

        InitializePlayerHUD();

        InitializeCurrencySystem();

        InitializeTickUpdater();

        InitializeLevelManager(character);
    }

    private void InitializePoolsManager() => _poolsManager.Initialize();

    private void InitializePlayerHUD() => _playerHUD.Initialize();

    private void InitializeTickUpdater() => _tickUpdater.Initialize();
    
    private Character CreateCharacter()
    {
        Character character = _characterSpawner.CreateCharacter();

        return character;
    }

    private void InitializeCamerasController(Character character)
    {
        _camerasController.Initialize();
        _camerasController.SetTargetForCamera(character);
    }


    private void InitializeCurrencySystem()
    {

        _currencyController.Initialize();
        _currencyDisplayController.Initialize();
    }

    private void InitializeLevelManager(Character character)
    {
        _levelManager.Construct(character, _waveController);

        _levelManager.Initialize();
    }
}
