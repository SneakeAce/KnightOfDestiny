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

    private UpgradeSystemController _upgradeSystemController;
    private UpgradeCardsView _cardsView;
    private UpgradeCardsViewController _cardsViewController;

    public FirstLevelBootstrap(IPoolsManager poolsManager, EnemyWaveController waveController,
        CamerasController camerasController, PlayerHUD playerHUD,
        ICurrencyController currencyController, CurrencyDisplayController currencyDisplayController,
        CharacterSpawner characterSpawner, TickUpdater tickUpdater, LevelController levelManager,
        UpgradeSystemController upgradeSystemController, UpgradeCardsView cardsView, 
        UpgradeCardsViewController cardsViewController)
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

        _upgradeSystemController = upgradeSystemController;
        _cardsView = cardsView;
        _cardsViewController = cardsViewController;

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

        InitializeUpgradeSystem(character);
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

    private void InitializeUpgradeSystem(Character character)
    {
        _cardsView.Initialize();

        _cardsViewController.Initialize();

        _upgradeSystemController.SetCharacterStatsManager(character.StatsManager);
        _upgradeSystemController.Initialize();
    }
}
