using Godot;
using System.Collections.Generic;

namespace Freecell;

public partial class Foundation : Stack
{
    // Mandatory properties.
    public override List<Card> CardsOnStack { get; set; } = [];
    public override string ObjectType { get; } = "FOUNDATION";
    public override Vector2 CardOffset { get; } = new(0, 0);

    public override bool CanAppendCard(int cardValue)
    {
        if (Card.GetCardColor(cardValue) != color) return false;
        if (Card.GetCardNumber(cardValue) > CardsOnStack.Count) return false;
        return true;
    }

    // Local properties.
    public int color = 0;

}
