using Godot;

public partial class VictoryScreen : Control
{
    private void _ready()
    {
        Visible = false;
    }

    private void _process(float delta)
    {
        // TODO: Add winning game logic here.
        // We can check the foundations every frame and see if they're all filled, but that's expensive.
        // Or we can expose a method that foundations call when they were filled to ehance performance.
    }

    private void Win()
    {
        // For now, just pause the game and show the victory screen.
        Visible = true;
        GetTree().Paused = true;
    }
}
