using Godot;
using System;

namespace Freecell;

public partial class UndoButton : ColorRect
{
    private bool _hovered = false;

    public void OnMouseEntered()
    {
        _hovered = true;
    }

    public void OnMouseExited()
    {
        _hovered = false;
    }

    private void _ready()
    {
        Connect("mouse_entered", Callable.From(OnMouseEntered));
        Connect("mouse_exited", Callable.From(OnMouseExited));
    }

    private void _process(float _)
    {
        Visible = HistoryManager.CanUndo;

        var isMouseButtonJustPressed = Input.IsActionJustPressed("leftMouse");

        if (isMouseButtonJustPressed && _hovered && HistoryManager.CanUndo)
        {
            GD.Print("Undo");
            HistoryManager.Undo();
        }
    }
}
