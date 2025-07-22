using Godot;
using System.Collections.Generic;

namespace Freecell;

public partial class Foundation : Area2D, IStack
{
  // Mandatory properties.
  public List<Card> CardsOnStack { get; set; } = [];
  public string ObjectType { get; } = "FOUNDATION";
  public Vector2 CardOffset { get; } = new(0, 0);

  public bool CanAppendCard(int cardValue)
  {
    if (Card.GetCardColor(cardValue) != color) return false;
    if (Card.GetCardNumber(cardValue) > CardsOnStack.Count) return false;
    return true;
  }

  // Local properties.
  public int color = 0;

}
