using Godot;
using System.Collections.Generic;

namespace Freecell;

public partial class Foundation : Stack
{
    // Mandatory properties.
    public override List<Card> CardsOnStack { get; set; } = [];
    public override string ObjectType { get; } = "FOUNDATION";
    public override Vector2 CardOffset { get; } = new(0, 0);

    // Local properties.
    [Export]
    public int color = 0;

    private Sprite2D _club;
    private Sprite2D _spade;
    private Sprite2D _heart;
    private Sprite2D _diamond;

    // Godot methods.
    public override void _Ready()
    {
        _club = GetNode<Sprite2D>("Club");
        _spade = GetNode<Sprite2D>("Spade");
        _heart = GetNode<Sprite2D>("Heart");
        _diamond = GetNode<Sprite2D>("Diamond");
        _club.Visible = false;
        _spade.Visible = false;
        _heart.Visible = false;
        _diamond.Visible = false;
        switch (color)
        {
            case 0:
                _heart.Visible = true;
                break;
            case 1:
                _diamond.Visible = true;
                break;
            case 2:
                _spade.Visible = true;
                break;
            case 3:
                _club.Visible = true;
                break;
        }
    }

    public override bool CanAppendCard(int cardValue)
    {
        if (Card.GetCardColor(cardValue) != color) return false;
        if (Card.GetCardNumber(cardValue) > CardsOnStack.Count) return false;
        return true;
    }
}
