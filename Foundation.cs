using Godot;
using System;

public partial class Foundation : Area2D, Pile
{
    // Mandatory properties.
    public string objectType = "FOUNDATION";
    public Vector2 cardOffset = new(0, 0);
    public Card[] cardsOnStack = [];

    // Local properties.
    public int color = 0;

    public bool canAppendCard(int cardValue)
    {
        if (Card.getCardColor(cardValue) != color) return false;
        if (Card.getCardNumber(cardValue) > cardsOnStack.size()) return false;
        return true;
    }
}

