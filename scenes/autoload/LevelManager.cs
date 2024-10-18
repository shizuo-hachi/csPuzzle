using System.Linq;
using Game.resources.level;
using Godot;

namespace Game.Autoload;

public partial class LevelManager : Node
{
    public static LevelManager Instance { get; private set; }
    
    [Export] private LevelDefinitionResource[] levelDefinitions;

    private int currentLevelIndex;
    
    public override void _Ready()
    {
        
    }
    
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            Instance = this;
        }
    }

    public static LevelDefinitionResource[] GetLevelDefinitions()
    {
        return Instance.levelDefinitions.ToArray();
    }

    public void ChangeToLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levelDefinitions.Length) return;
        currentLevelIndex = levelIndex;
        
        var levelDefinition = levelDefinitions[currentLevelIndex];
        GetTree().ChangeSceneToFile(levelDefinition.LevelScenePath);
    }

    public void ChangeToNextLevel()
    {
        ChangeToLevel(currentLevelIndex + 1);
    }
    
}
