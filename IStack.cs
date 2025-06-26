using Godot;
using System.Collections.Generic;

namespace Freecell;

public interface IStack
{
    List<Card> CardsOnStack { get; }
    string ObjectType { get; }
    Vector2 CardOffset { get; }
    bool CanAppendCard(int cardValue);
}
