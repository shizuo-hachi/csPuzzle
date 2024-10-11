using Godot;

namespace Game.UI;

public partial class PauseScreen : Control
{
    public override void _Notification(int what)
    {
        if (what == NotificationWMMouseExit)
        {
            GetTree().Paused = true;
            Visible = true;
        }
        else if (what == NotificationWMMouseEnter)
        {
            GetTree().Paused = false;
            Visible = false;

        }
    }
}
