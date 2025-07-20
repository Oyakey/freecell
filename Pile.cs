using Godot;
using System;

// public partial class Foundation : Node
public interface Pile
{
    public int CardsOnStack { get; }
    public void AddCard(int cardValue);
    public void RemoveCard(int cardValue);
    public string ObjectType;
    public Vector2 CardOffset;
}

enum ObjectType
{
    Stack,
    Foundation,
    Freecell,
    Card
}
