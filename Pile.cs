using Godot;
using System.Collections.Generic;

public interface Pile
{
    public List<Card> CardsOnStack { get; }
    public string ObjectType { get; }
    public Vector2 CardOffset { get; }
    // public void AddCard(int cardValue);
    // public void RemoveCard(int cardValue);
}

enum ObjectType
{
    Stack,
    Foundation,
    Freecell,
    Card
}
