using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Freecell;

public partial class Freecell : Area2D, IStack
{
    // Mandatory properties.
    public List<Card> CardsOnStack { get; set; } = [];
    public string ObjectType { get; } = "FREECELL";
    public Vector2 CardOffset { get; } = new(0, 0);

    public bool CanAppendCard(int cardValue)
    {
        return CardsOnStack.Count <= 0;
    }
}
