using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private PlayerHUD _playerHUDPrefab;

    public override void InstallBindings()
    {
        BindPlayerHUD();
    }

    private void BindPlayerHUD()
    {
        PlayerHUD playerHUD = Container.InstantiatePrefabForComponent<PlayerHUD>(_playerHUDPrefab);

        if (playerHUD == null)
        {
            Debug.LogError("PlayerHUD in FirstLevelInstaller is null!");
            return;
        }

        Container.Bind<PlayerHUD>()
            .FromInstance(playerHUD)
            .AsSingle();
    }

}
