using Godot;
using System;
using System.Collections.Generic;

namespace freecell;

public partial class Foundation : Area2D, IPile
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

  public bool CanAppendCard(int cardValue)
  {
	if (Card.GetCardColor(cardValue) != color) return false;
	if (Card.GetCardNumber(cardValue) > cardsOnStack.Count) return false;
	return true;
  }
}
