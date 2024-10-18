using Godot;

namespace Game.Building;

public partial class BuildingGhost : Node2D
{
	private Node2D topLeft;
	private Node2D bottomLeft;
	private Node2D topRight;
	private Node2D bottomRight;
	private Node2D upDownRoot;
	private Node2D spriteRoot;

	private Tween spriteTween;

	public override void _Ready()
	{
		topLeft = GetNode<Node2D>("TopLeft");
		bottomLeft = GetNode<Node2D>("BottomLeft");
		topRight = GetNode<Node2D>("TopRight");
		bottomRight = GetNode<Node2D>("BottomRight");
		spriteRoot = GetNode<Node2D>("SpriteRoot");
		upDownRoot = GetNode<Node2D>("%UpDownRoot");

		var upDownTween = CreateTween();
		upDownTween.SetLoops(0);
		upDownTween.TweenProperty(upDownRoot, "position", Vector2.Down * 6, .3)
			.SetEase(Tween.EaseType.InOut)
			.SetTrans(Tween.TransitionType.Quad);
		upDownTween.TweenProperty(upDownRoot, "position", Vector2.Up * 6, .3)
			.SetEase(Tween.EaseType.InOut)
			.SetTrans(Tween.TransitionType.Quad);
	}
	
	public void SetInvalid(Sprite2D sprite)
	{
		sprite.SelfModulate = Colors.Red;
		Color alpha = sprite.SelfModulate;
		alpha.A = 0.7f;
		sprite.SelfModulate = alpha;
	}

	public void SetValid(Sprite2D sprite)
	{
		sprite.SelfModulate = Colors.SkyBlue;
		Color alpha = sprite.SelfModulate;
		alpha.A = 0.7f;
		sprite.SelfModulate = alpha;
	}

	public void SetDimensions(Vector2I dimensions)
	{
		bottomLeft.Position = dimensions * new Vector2I(0, 64);
		bottomRight.Position = dimensions * new Vector2I(64, 64);
		topRight.Position = dimensions * new Vector2I(64, 0);
	}

	public void AddSpriteNode(Node2D spriteNode)
	{
		upDownRoot.AddChild(spriteNode);
	}

	public void DoHoverAnimation()
	{
		if (spriteTween != null && spriteTween.IsValid())
		{
			spriteTween.Kill();
		}
		spriteTween = CreateTween();
		spriteTween
			.TweenProperty(spriteRoot, "global_position", GlobalPosition, .3)
			.SetTrans(Tween.TransitionType.Back)
			.SetEase(Tween.EaseType.Out);
	}
}
