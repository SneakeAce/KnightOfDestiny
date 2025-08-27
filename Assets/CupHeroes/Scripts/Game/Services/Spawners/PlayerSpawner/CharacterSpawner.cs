using System;
using Zenject;

public class CharacterSpawner
{
    private DiContainer _container;
    private IConfigsProvider _configsProvider;
    private IEntityBuilder _entityBuilder;

    public CharacterSpawner(DiContainer container, IConfigsProvider configsProvider, IEntityBuilder entityBuilder)
    {
        _container = container;
        _configsProvider = configsProvider;
        _entityBuilder = entityBuilder;
    }

    public Character CreateCharacter()
    {
        CharacterConfig config = GetCharacterConfig();

        if (config == null)
            throw new ArgumentNullException($"This character config = {config} is null!");

        Character character = _container.InstantiatePrefabForComponent<Character>(
            config.Prefab,
            config.SpawnPosition,
            config.SpawnRotation,
            null);

        IEntity charEntity = character.GetComponent<IEntity>();

        charEntity.SetConfig(config);
        charEntity.Initialize();

        _entityBuilder.BuildEntity(ref charEntity);

        if (character == null)
            throw new ArgumentNullException("Character in CharacterSpawner is null!");

        BindCharacter(character);

        return (Character)charEntity;
    }

    private CharacterConfig GetCharacterConfig()
    {
        return _configsProvider.GetSingleConfig<CharacterConfig>().GetConfig<CharacterConfig>();
    }

    private void BindCharacter(Character character)
    {
        _container.BindInterfacesAndSelfTo<Character>()
            .FromInstance(character)
            .AsSingle();
    }

}
