using Godot;
using System;

public partial class Foundation : Area2D, Pile
{
    // Mandatory properties.
    public const string objectType = "FOUNDATION";
    public const Vector2 cardOffset = new Vector2(0, 0);
    public List<Card> cardsOnStack = new List<Card>();

    // Local properties.
    @export public int color = 0;

    public boolean canAppendCard(int cardValue)
    {
        if (Card.getCardColor(cardValue) != color) return false;
        if (Card.getCardNumber(cardValue) > cardsOnStack.size()) return false;
        return true;
    }
}

