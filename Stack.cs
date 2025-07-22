using System.Collections.Generic;
using Godot;

namespace Freecell;


public abstract partial class Stack : Area2D
{
    public List<Card> CardsOnStack { get; }
    public abstract string ObjectType { get; }
    public abstract Vector2 CardOffset { get; }
    public abstract bool CanAppendCard(int cardValue);
}
