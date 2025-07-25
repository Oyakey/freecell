using System;
using System.Collections.Generic;
using Godot;

namespace Freecell;

public partial class GameManager : Node2D
{
    private PackedScene cardScene = ResourceLoader.Load<PackedScene>("res://card.tscn");
    private Cascade[] cascades = [];

    // Called when the node enters the scene tree for the first time.
    private void _ready()
    {
        SetCascades();
        InitGame();
    }

    // Set references to all Cascade nodes.
    private void SetCascades()
    {
        cascades =
        [
            GetNode<Cascade>("Stack"),
            GetNode<Cascade>("Stack2"),
            GetNode<Cascade>("Stack3"),
            GetNode<Cascade>("Stack4"),
            GetNode<Cascade>("Stack5"),
            GetNode<Cascade>("Stack6"),
            GetNode<Cascade>("Stack7"),
            GetNode<Cascade>("Stack8"),
        ];
    }

    private void InitGame()
    {
        int cardDeckSize = 52; // 52 cards.
        List<int> cardValues = [];

        for (int i = 0; i <= cardDeckSize; i++)
        {
            cardValues.Add(i);
        }

        Shuffle(cardValues);
        for (int cascadeIndex = 0; cascadeIndex <= cascades.Length; cascadeIndex++)
        {
            var cardsOnStackCount = cascadeIndex < 4 ? 7 : 6;
            for (int i = 0; i < cardsOnStackCount; i++)
            {
                SpawnCard(cardValues[0], cascades[cascadeIndex]);
                cardValues.RemoveAt(0);
            }
        }
    }

    private void SpawnCard(int cardValue, Cascade stack)
    {
        var card = cardScene.Instantiate<Card>();
        card.CardValue = cardValue;
        card.Stack = stack;
        card.Order = stack.CardsOnStack.Count;

        stack.CardsOnStack.Add(card);

        card.TeleportToStack();
        AddChild(card);
    }

    private static void Shuffle(List<int> array)
    {
        Random rng = new Random();
        int n = array.Count;

        for (int i = n - 1; i > 0; i--)
        {
            // Random values between 0 and 1.
            int j = rng.Next(i + 1);
            // Swap array[i] and array[j].
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
