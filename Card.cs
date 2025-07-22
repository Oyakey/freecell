using System.Collections.Generic;
using Freecell;
using Godot;

namespace Freecell;

public partial class Card : Area2D
{
    public const int CardCountByColor = 13;
    public const int CARD_COUNT_BY_COLOR = 13;

    public string ObjectType = "CARD";
    public int CardValue = 0;

    private AnimatedSprite2D _animated_sprite;
    private Area2D stack = null;
    private int order = 0;
    private List<Area2D> collidingStacks = [];
    private bool snapping = false;
    private bool dragged = false;

    // Static methods.
    public static int GetCardNumber(int value)
    {
        return value % CardCountByColor;
    }

    public static int GetCardColor(int value)
    {
        return Mathf.FloorToInt(value / CardCountByColor);
    }

    public static bool IsCardRed(int value)
    {
        var color = GetCardColor(value);
        // Red colors are 0, 1; Black colors are 2, 3.
        // TODO: replace with an enum, or remove magic numbers.
        return color == 0 || color == 1;
    }

    // Private methods.
    private void _ready()
    {
        _animated_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animated_sprite.SetFrameAndProgress(CardValue, 0);
        ZIndex = order;
    }

    private void _process()
    {
        if (dragged)
        {
            dragged = false;
            snapToStack();
        }
    }

    private void snapToPos(Vector2 pos)
    {
        var tween = GetTree().CreateTween();
        tween.TweenProperty(GetNode("."), "position", new Vector2(pos.X, pos.Y), .15);
    }

    private void snapToStack()
    {
        if (stack == null) return;
        // snapToPos(stack.Position + stack.CardOffset * order);
    }

    private void teleportToStack()
    {
        if (stack == null) return;
        // Position = stack.Position + stack.CardOffset * order;
    }

    private void _on_area_entered(Area2D area)
    {
        if (area is Stack) collidingStacks.Add(area);
    }

    private void _on_area_exited(Area2D area)
    {
        collidingStacks.Remove(area);
    }

    private Area2D getClosestStack()
    {
        Area2D closestStack = null;
        foreach (Area2D collidingStack in collidingStacks)
        {

            if (closestStack == null)
            {
                closestStack = collidingStack;
                continue;
            }
            var collidingStackDist = collidingStack.Position - Position;


            var closestDist = closestStack.Position - Position;

            if (collidingStackDist.Length() <= closestDist.Length()) closestStack = collidingStack;
        }
        return closestStack;
    }

    private void addToStack()
    {
        dragged = true;
        var newStack = getClosestStack();

        // Check if card can be added to stack.
        if (newStack == null) return;
        // if (!newStack.CanAppendCard(CardValue)) return;
        if (stack == null) return;

        // Proceed to add card to stack.
        // stack.CardsOnStack.Remove(GetNode<Card>("."));
        stack = newStack;
        // order = newStack.CardsOnStack.Count;
        // newStack.CardsOnStack.Add(GetNode<Card>("."));
    }

    private bool canMoveCard()
    {
        // if (stack.ObjectType == "STACK" && stack.CardsOnStack.Count > order + 1) return false;
        return true;
    }
}
