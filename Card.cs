using Godot;
using System;

public class Card : Node2D
{
    static int cardCountByColor = 13;

    static int getCardNumber(int value)
    {
        return value % cardCountByColor;
    }

    static int getCardColor(int value)
    {
        return Mathf.FloorToInt(value / cardCountByColor);
    }

    static bool isCardRed(int value)
    {
        // Red colors are 0, 1; Black colors are 2, 3.
        return getCardColor(value) == 0 || getCardColor(value) == 1;
    }

}
