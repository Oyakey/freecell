using System.Collections.Generic;
using Godot;

namespace Freecell;

public partial class Cursor : Area2D
{
    public List<Area2D> CollidingAreas = [];
    public Card dragging = null;
    private int zindex = 50;
    private Vector2 offset;

    private void _process(float _)
    {
        // follow mouse cursor.
        var mousePosition = GetViewport().GetCamera2D().GetGlobalMousePosition();
        Position = mousePosition;

        var colliding = getFirstCollider();

        if (colliding is Card cardArea && !cardArea.Hovered)
        {
            cardArea.ShowOutline();
        }

        if (dragging != null)
        {
            if (Input.IsActionPressed("leftMouse"))
            {
                dragging.Position = mousePosition + offset;
            }
            else
            {
                dragging.AddToStack();
                dragging.HideDragging();
                dragging = null;
            }
        }
        else
        {
            if (colliding != null && Input.IsActionPressed("leftMouse"))
            {
                if (colliding is not Card)
                    return;
                var collidingCard = (Card)colliding;
                if (collidingCard.CanMoveCard())
                {
                    dragging = collidingCard;
                    zindex++;
                    dragging.ZIndex = zindex;
                    offset = dragging.Position - Position;
                    dragging.ShowDragging();
                }
            }
        }
    }

    private void _on_area_entered(Area2D area)
    {
        if (area is Card)
            CollidingAreas.Add(area);
    }

    private void _on_area_exited(Area2D area)
    {
        if (area is Card cardArea && cardArea.Hovered)
        {
            cardArea.HideOutline();
        }
        CollidingAreas.Remove(area);
    }

    private Area2D getFirstCollider()
    {
        Area2D areaOnTop = null;
        foreach (var area in CollidingAreas)
        {
            if (areaOnTop == null || areaOnTop.ZIndex < area.ZIndex)
            {
                areaOnTop = area;
                continue;
            }
        }
        return areaOnTop;
    }
}
