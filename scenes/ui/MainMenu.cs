using Game.UI;
using Godot;

public partial class MainMenu : Node
{
    private Button playButton;
    private Button quitButton;
    private Control mainMenuContainer;
    private LevelSelectScreen levelSelectScreen;

    public override void _Ready()
    {
        playButton = GetNode<Button>("%PlayButton");
        quitButton = GetNode<Button>("%QuitButton");
        mainMenuContainer = GetNode<Control>("%MainMenuContainer");
        levelSelectScreen = GetNode<LevelSelectScreen>("%LevelSelectScreen");

        levelSelectScreen.Visible = false;
        mainMenuContainer.Visible = true;

        playButton.Pressed += OnPlayButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
        levelSelectScreen.BackPressed += OnLevelSelectBackPressed;
    }

    public void OnPlayButtonPressed()
    {
        mainMenuContainer.Visible = false;
        levelSelectScreen.Visible = true;
    }

    public void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnLevelSelectBackPressed()
    {
        mainMenuContainer.Visible = true;
        levelSelectScreen.Visible = false;
    }
}
