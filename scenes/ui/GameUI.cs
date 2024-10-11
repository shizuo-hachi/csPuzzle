using Game.Manager;
using Game.Resources.Building;
using Godot;

namespace Game.UI;

public partial class GameUI : CanvasLayer
{
	[Signal] public delegate void BuildingResourceSelectedEventHandler(BuildingResource buildingResource);

	private VBoxContainer buildingSectionContainer;
	private Label resourceLabel;
	private PauseScreen pauseScreen;

	[Export] private BuildingManager buildingManager;
	[Export] private BuildingResource[] buildingResources;
	[Export] private PackedScene buildingSectionScene;
	[Export] private PackedScene pauseScreenScene;

	public override void _Ready()
	{
		buildingSectionContainer = GetNode<VBoxContainer>("%BuildingSectionContainer");
		resourceLabel = GetNode<Label>("%ResourceLabel");
		pauseScreen = pauseScreenScene.Instantiate<PauseScreen>();
		AddChild(pauseScreen);
		CreateBuildingSections();

		buildingManager.AvailableResourceCountChanged += OnAvailableResourceCountChanged;
	}
	
	private void CreateBuildingSections()
	{
		foreach (var buildingResource in buildingResources)
		{
			var buildingSection = buildingSectionScene.Instantiate<BuildingSection>();
			buildingSectionContainer.AddChild(buildingSection);
			buildingSection.SetBuildingResource(buildingResource);

			buildingSection.SelectButtonPressed += () =>
			{
				EmitSignal(SignalName.BuildingResourceSelected, buildingResource);
			};
		}
	}

	public void HideUI()
	{
		Visible = false;
	}

	private void OnAvailableResourceCountChanged(int availableResourceCount)
	{
		resourceLabel.Text = availableResourceCount.ToString();
	}
}
