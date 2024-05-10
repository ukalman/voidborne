namespace Utilities
{
    
    public enum UIPanelTypes
    {
        MainMenu, // Start
        Settings,
        CharacterCreation,
        Level,
        Pause,
        Win,
        Lose
    }
    
    public enum GameState
    {
        None,
        Start, // MainMenu (Settings, Credits, Controls, etc.)
        CharacterCreation,
        GenerateGrid,
        Battle,
        LevelPrep,
        Gameplay,
        SpawnHeroes,
        SpawnEnemies,
        HeroesTurn,
        EnemiesTurn,
        LevelEnd,
        Pause,
        Win,
        Lose
    }

    public enum Faction
    {
        Hero,
        Enemy
    }
    
    public enum ModuleState
    {
        Uninitialized,
        Initialized,
        PostInitialized,
        Activated,
        Deactivated,
        Disabled,
        Paused,
        Resumed,
        Restarted
    }

    public enum PlayerState
    {
        None,
        Idle,
        Move,
        Inventory,
        Attack,
        Death
    }

    public enum HeroType
    {
        Knight,
        Archer,
        Mage
    }
    
}