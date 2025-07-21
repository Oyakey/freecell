using Godot;
using System;

public class Card : Node2D
{
    public static int cardCountByColor = 13;

    public static int getCardNumber(int value)
    {
        return value % cardCountByColor;
    }

    public static int getCardColor(int value)
    {
        return Mathf.FloorToInt(value / cardCountByColor);
    }

    public static bool isCardRed(int value)
    {
        // Red colors are 0, 1; Black colors are 2, 3.
        return getCardColor(value) == 0 || getCardColor(value) == 1;
    }

}
