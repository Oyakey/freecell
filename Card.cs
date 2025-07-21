using Godot;
using System;

namespace freecell;

public partial class Card : Node2D
{
  public const int CardCountByColor = 13;

  public static int GetCardNumber(int value)
  {
    return value % CardCountByColor;
  }

  public static int GetCardColor(int value)
  {
    return Mathf.FloorToInt(value / CardCountByColor);
  }

  public static bool IsCardRed(int value)
  {
    // Red colors are 0, 1; Black colors are 2, 3.
    return GetCardColor(value) == 0 || GetCardColor(value) == 1;
  }

}
