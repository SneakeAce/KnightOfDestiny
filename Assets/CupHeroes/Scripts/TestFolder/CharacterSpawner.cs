using System;
using Zenject;

public class CharacterSpawner
{
    private DiContainer _container;
    private IConfigsProvider _configsProvider;

    public CharacterSpawner(DiContainer container, IConfigsProvider configsProvider)
    {
        _container = container;
        _configsProvider = configsProvider;
    }

    public void CreateCharacter()
    {
        CharacterConfig config = GetCharacterConfig();

        if (config == null)
            throw new ArgumentNullException($"This character config = {config} is null!");

        Character character = _container.InstantiatePrefabForComponent<Character>(
            config.Prefab,
            config.SpawnPosition,
            config.SpawnRotation,
            null);

        if (character == null)
            throw new ArgumentNullException("Character in CharacterSpawner is null!");

        BindCharacter(character);
    }

    private CharacterConfig GetCharacterConfig()
    {
        return (CharacterConfig)_configsProvider.GetSingleConfig<CharacterConfig>();
    }

    private void BindCharacter(Character character)
    {
        _container.BindInterfacesAndSelfTo<Character>()
            .FromInstance(character)
            .AsSingle();
    }

}
