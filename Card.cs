using System.Collections.Generic;
using Godot;

namespace Freecell;

public partial class Card : Area2D
{
	public const int CardCountByColor = 13;
	public const int CARD_COUNT_BY_COLOR = 13;
	public List<Stack> CollidingStacks { get; set; } = [];
	public bool Hovered
	{
		get => CanMoveCard() && _outline.Visible;
		set => _outline.Visible = CanMoveCard() && value;
	}

	public string ObjectType = "CARD";
	public int CardValue = 0;
	public Stack Stack = null;
	public int Order = 0;

	private AnimatedSprite2D _animated_sprite;
	private bool _snapping = false;
	private bool _dragged = false;
	private Sprite2D _outline;

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
		_outline = GetNode<Sprite2D>("Outline");
		_outline.Visible = false;
		_animated_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animated_sprite.SetFrameAndProgress(CardValue, 0);
		ZIndex = Order;
	}

	private void _process(float _)
	{
		if (_dragged)
		{
			_dragged = false;
			SnapToStack();
		}
	}

	// Public methods.
	public void AddToStack()
	{
		_dragged = true;
		var newStack = GetClosestStack();

		// Check if card can be added to stack.
		if (newStack == null)
			return;
		if (!newStack.CanAppendCard(CardValue))
			return;
		if (Stack == null)
			return;

		// Proceed to add card to stack.
		Stack.CardsOnStack.Remove(GetNode<Card>("."));
		Stack = newStack;
		Order = newStack.CardsOnStack.Count;
		newStack.CardsOnStack.Add(GetNode<Card>("."));
	}

	public bool CanMoveCard()
	{
		if (Stack is Cascade && Stack.CardsOnStack.Count > Order + 1)
			return false;
		return true;
	}

	// Private methods.
	private void SnapToPos(Vector2 pos)
	{
		var tween = GetTree().CreateTween();
		tween.TweenProperty(GetNode("."), "position", new Vector2(pos.X, pos.Y), .15);
	}

	private void SnapToStack()
	{
		if (Stack == null)
			return;
		SnapToPos(Stack.Position + Stack.CardOffset * Order);
	}

	public void TeleportToStack()
	{
		if (Stack == null)
			return;
		Position = Stack.Position + Stack.CardOffset * Order;
	}

	private void _on_area_entered(Stack area)
	{
		CollidingStacks.Add(area);
	}

	private void _on_area_exited(Stack area)
	{
		CollidingStacks.Remove(area);
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
