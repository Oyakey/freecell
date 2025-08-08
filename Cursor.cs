using System.Collections.Generic;
using Godot;

namespace Freecell;

public partial class Cursor : Area2D
{
    private Card _draggingCard;
    private int _zindex = 50; // Defaults to 50, that may not be necessary.
    private Vector2 _offset;
    private readonly List<Card> _collidingCards = [];
    private Card _firstCollidingCard;
    private const float _quickClickTiming = 0.2f; // In seconds.
    private float _quickClickEndTime;

    // Godot methods.

    private void _process(float _)
    {
        HandleHover();
        HandlePosition();
        HandleDragging();
    }

    // Godot signal handlers.

    private void _on_area_entered(Area2D area)
    {
        if (area is not Card card)
        {
            return;
        }

        _collidingCards.Add(card);

        // Update first colliding card.
        if (_firstCollidingCard == null || _firstCollidingCard.ZIndex < card.ZIndex)
        {
            _firstCollidingCard = card;
        }
    }

    private void _on_area_exited(Area2D area)
    {
        if (area is not Card card)
            return;

        if (card.Hovered)
        {
            card.HideOutline();
        }

        _collidingCards.Remove(card);

        if (card == _firstCollidingCard)
        {
            _firstCollidingCard = GetFirstCollidingCard();
        }
    }

    // Private methods.

    private void HandlePosition()
    {
        // Make node follow mouse cursor.
        var mousePosition = GetViewport().GetCamera2D().GetGlobalMousePosition();
        Position = mousePosition;
    }

    private void HandleDragging()
    {
        var isDragging = _draggingCard != null;
        var isMouseButtonPressed = Input.IsActionPressed("leftMouse");

        if (isDragging)
        {
            if (!isMouseButtonPressed)
            {
                // If the player held the card for a very short time, smart move it.
                if (Time.GetTicksMsec() <= _quickClickEndTime)
                {
                    SmartMove();
                    return;
                }
                StopDragging();
                return;
            }
            CardFollowCursor();
            return;
        }

        if (
            isMouseButtonPressed &&
            _firstCollidingCard != null &&
            _firstCollidingCard.CanMoveCard()
        )
        {
            StartDragging(_firstCollidingCard);
            _quickClickEndTime = Time.GetTicksMsec() + _quickClickTiming * 1000;
        }
    }

    private void HandleHover()
    {
        var isDragging = _draggingCard != null;

        if (isDragging) return;

        _firstCollidingCard?.ShowOutline();
    }

    private void StartDragging(Card card)
    {
        _draggingCard = card;
        _zindex++;
        _draggingCard.ZIndex = 100;
        _offset = _draggingCard.Position - Position;
        _draggingCard.ShowDragging();
    }

    private void StopDragging()
    {
        _draggingCard.AddToClosestStack();
        _draggingCard.StopDragging();
        _draggingCard = null;
    }

    private void CardFollowCursor()
    {
        _draggingCard.Position = Position + _offset;
    }

    private void SmartMove()
    {
        if (_firstCollidingCard == null || !_firstCollidingCard.CanMoveCard())
        {
            return;
        }

        var bestStackToMoveTo = GameManager.FindBestValidStackForCard(_draggingCard);
        if (bestStackToMoveTo != null)
        {
            _draggingCard.AddToStack(bestStackToMoveTo);
            _draggingCard.StopDragging();
            _draggingCard = null;
        }
    }

    private Card GetFirstCollidingCard()
    {
        Card areaOnTop = null;
        foreach (var area in _collidingCards)
        {
            if (areaOnTop == null || areaOnTop.ZIndex < area.ZIndex)
            {
                areaOnTop = area;
            }
        }
        return areaOnTop;
    }
}
