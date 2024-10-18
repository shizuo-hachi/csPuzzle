using Godot;

namespace Game;

public partial class GameCamera : Camera2D
{
	private const int TILE_SIZE = 64;
	private const float PAN_SPEED = 500;

	private readonly StringName ACTION_PAN_LEFT = "pan_left";
	private readonly StringName ACTION_PAN_RIGHT = "pan_right";
	private readonly StringName ACTION_PAN_UP = "pan_up";
	private readonly StringName ACTION_PAN_DOWN = "pan_down";

	private Rect2 viewport;

	public override void _Ready()
	{
		viewport = GetViewportRect();
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GetScreenCenterPosition();
		var movementVector = Input.GetVector(ACTION_PAN_LEFT, ACTION_PAN_RIGHT, ACTION_PAN_UP, ACTION_PAN_DOWN);
		GlobalPosition += movementVector * PAN_SPEED * (float)delta;
	}

	public void SetBoundingRect(Rect2I boundingRect)
	{
		LimitLeft = boundingRect.Position.X * TILE_SIZE;
		LimitRight = boundingRect.End.X * TILE_SIZE;
		LimitTop = boundingRect.Position.Y * TILE_SIZE;
		LimitBottom = boundingRect.End.Y * TILE_SIZE;
	}

	public void CenterOnPosition(Vector2 position)
	{
		GlobalPosition = position;
	}

	public override void _Input(InputEvent @event)
	{
		
		if (!DisplayServer.WindowIsFocused())
		{
			DisplayServer.MouseSetMode(DisplayServer.MouseMode.Captured);
		}
		else
		{
			DisplayServer.MouseSetMode(DisplayServer.MouseMode.Visible);
		}
	}
}
