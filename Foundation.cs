using Godot;
using System.Collections.Generic;

public partial class Foundation : Area2D, Pile
{
    // Mandatory properties.
    List<Card> cardsOnStack = new();
    public List<Card> CardsOnStack
    {
        get { return cardsOnStack; }
    }
    public string ObjectType
    {
        get { return "FOUNDATION"; }
    }
    public Vector2 CardOffset
    {
        get { return new(0, 0); }
    }

    // Local properties.
    public int color = 0;

    public bool canAppendCard(int cardValue)
    {
        if (Card.getCardColor(cardValue) != color) return false;
        if (Card.getCardNumber(cardValue) > cardsOnStack.Count) return false;
        return true;
    }
}

