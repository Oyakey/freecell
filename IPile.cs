using Godot;
using System.Collections.Generic;

namespace freecell;

public interface IPile
{
  public List<Card> CardsOnStack { get; }
  public string ObjectType { get; }
  public Vector2 CardOffset { get; }
  public bool CanAppendCard(int cardValue);
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
