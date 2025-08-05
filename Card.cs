using System.Collections.Generic;
using Godot;

namespace Freecell;

public partial class Card : Area2D
{
    public const int CardCountByColor = 13;
    public const int CARD_COUNT_BY_COLOR = 13;
    public List<Stack> CollidingStacks { get; set; } = [];
    public bool Hovered { get => CanMoveCard() && _outline.Visible; }

    public string ObjectType = "CARD";
    public int CardValue = 0;
    public Stack CurrentStack = null;
    public int Order = 0;

    private AnimatedSprite2D _cardNode;
    private bool _snapping = false;
    private bool _dragged = false;
    private Outline _outline;
    private Sprite2D _shadow;
    private Vector2 defaultScale;

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

    // Godot methods.

    private void _ready()
    {
        defaultScale = Scale;

        ZIndex = Order;

        _outline = GetNode<Outline>("Outline");
        _outline.Visible = false;

        _cardNode = GetNode<AnimatedSprite2D>("Card");
        _cardNode.SetFrameAndProgress(CardValue, 0);

        _shadow = GetNode<Sprite2D>("Shadow");
        _shadow.Visible = false;
    }

    private void _process(float _)
    {
        if (_dragged)
        {
            _dragged = false;
            SnapToCurrentStack();
        }
    }

    // Public methods.

    public void AddToClosestStack()
    {
        var newStack = GetClosestStack();
        AddToStack(newStack);
    }

    public void AddToStack(Stack stack)
    {
        _dragged = true;

        // Check if card can be added to stack.
        if (stack == null || !stack.CanAppendCard(CardValue) || CurrentStack == null)
            return;

        // Proceed to add card to stack.
        CurrentStack.CardsOnStack.Remove(this);
        CurrentStack = stack;
        Order = stack.CardsOnStack.Count;
        stack.CardsOnStack.Add(this);
    }

    public bool CanMoveCard()
    {
        if (CurrentStack is Cascade && CurrentStack.CardsOnStack.Count > Order + 1)
            return false;
        return true;
    }

    public void ShowOutline()
    {
        if (Hovered || !CanMoveCard())
            return;
        _outline.Visible = true;
        _outline.PlayShowAnimation();
    }
    public void HideOutline()
    {
        if (!Hovered)
            return;
        _outline.Visible = false;
        // TODO: Add a hide animation.
    }

    public void TeleportToStack()
    {
        if (CurrentStack == null)
            return;
        Position = CurrentStack.Position + CurrentStack.CardOffset * Order;
    }

    public void ShowDragging()
    {
        if (_shadow.Visible)
            return;

        float transitionDuration = .15f;
        float scaleTransitionFactor = 1.05f;
        Vector2 shadowFinalPosition = new Vector2(6f, 6f);
        var tween = GetTree().CreateTween();

        _shadow.Visible = true;

        // Play all animations in parallel.
        tween.Parallel().TweenProperty(_cardNode, "scale", defaultScale * scaleTransitionFactor, transitionDuration).SetEase(Tween.EaseType.InOut);
        tween.Parallel().TweenProperty(_outline, "scale", _outline.DefaultScale * scaleTransitionFactor, transitionDuration).SetEase(Tween.EaseType.InOut);
        tween.Parallel().TweenProperty(_shadow, "position", shadowFinalPosition, transitionDuration).SetEase(Tween.EaseType.InOut);
    }

    public void HideDragging()
    {
        if (!_shadow.Visible)
            return;

        float transitionDuration = .15f;
        var tween = GetTree().CreateTween();

        // Play all animations in parallel.
        tween.Parallel().TweenProperty(_cardNode, "scale", defaultScale, transitionDuration).SetEase(Tween.EaseType.InOut);
        tween.Parallel().TweenProperty(_outline, "scale", _outline.DefaultScale, transitionDuration).SetEase(Tween.EaseType.InOut);
        tween.Parallel().TweenProperty(_shadow, "position", Vector2.Zero, transitionDuration).SetEase(Tween.EaseType.InOut);

        // Then, hide the shadow.
        tween.TweenCallback(Callable.From(() => _shadow.Visible = false));
    }

    // Private methods.

    private void SnapToPos(Vector2 pos)
    {
        var tween = GetTree().CreateTween();
        tween.TweenProperty(this, "position", new Vector2(pos.X, pos.Y), .15);
    }

    private void SnapToCurrentStack()
    {
        if (CurrentStack == null)
            return;
        SnapToPos(CurrentStack.Position + CurrentStack.CardOffset * Order);
    }

    private void _on_area_entered(Area2D area)
    {
        if (area is Stack stack)
            CollidingStacks.Add(stack);
    }

    private void _on_area_exited(Area2D area)
    {
        if (area is Stack stack)
            CollidingStacks.Remove(stack);
    }

    private Stack GetClosestStack()
    {
        Stack closestStack = null;
        foreach (var collidingStack in CollidingStacks)
        {
            if (closestStack == null)
            {
                closestStack = collidingStack;
                continue;
            }
            var collidingStackDist = collidingStack.Position - Position;

            var closestDist = closestStack.Position - Position;

            if (collidingStackDist.Length() <= closestDist.Length())
                closestStack = collidingStack;
        }
        return closestStack;
    }
}
