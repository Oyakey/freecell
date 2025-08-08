using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace Freecell;

public partial class GameManager : Node2D
{
    private static Cascade[] cascades = [];
    private static Freecell[] freecells = [];
    private static Foundation[] foundations = [];

    // Called when the node enters the scene tree for the first time.
    private void _ready()
    {
        SetCascades();
        SetFreecells();
        SetFoundations();
        InitGame();
    }

    public static Stack FindBestValidStackForCard(Card card)
    {
        // Prioritize foundations.
        var validFoundations = FindValidStacksForCard(card, foundations);
        if (validFoundations.Count > 0)
            return validFoundations[0];

        var validCascades = FindValidStacksForCard(card, cascades);

        // Then look for a cascade with cards on stack.
        foreach (var cascade in validCascades)
        {
            if (cascade.CardsOnStack.Count > 0)
                return cascade;
        }

        // Then look for a valid freecell.
        var validFreecells = FindValidStacksForCard(card, freecells);
        if (validFreecells.Count > 0)
            return validFreecells[0];

        // Finally, look for a valid empty cascade.
        if (validCascades.Count > 0)
            return validCascades[0];

        return null;
    }

    private static List<Stack> FindValidStacksForCard(Card card, Stack[] stacks)
    {
        List<Stack> validStacks = [];
        foreach (var stack in stacks)
        {
            if (card.CurrentStack != stack && stack.CanAppendCard(card.CardValue))
            {
                validStacks.Add(stack);
            }
        }
        return validStacks;
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

    private void SetFreecells()
    {
        freecells = [
            GetNode<Freecell>("Freecell"),
            GetNode<Freecell>("Freecell2"),
            GetNode<Freecell>("Freecell3"),
            GetNode<Freecell>("Freecell4"),
        ];
    }

    private void SetFoundations()
    {
        foundations = [
            GetNode<Foundation>("Foundation Hearts"),
            GetNode<Foundation>("Foundation Clubs"),
            GetNode<Foundation>("Foundation Diamonds"),
            GetNode<Foundation>("Foundation Spades"),
        ];
    }

    private void InitGame()
    {
        int cardDeckSize = 52; // 52 cards.
        List<int> cardValues = [];

        for (int i = 0; i < cardDeckSize; i++)
        {
            cardValues.Add(i);
        }

        Shuffle(cardValues);
        for (int cascadeIndex = 0; cascadeIndex < cascades.Length; cascadeIndex++)
        {
            var cardsOnStackCount = cascadeIndex < 4 ? 7 : 6;
            for (int i = 0; i < cardsOnStackCount; i++)
            {
                SpawnCard(cardValues[0], cascades[cascadeIndex]);
                cardValues.RemoveAt(0);
            }
        }
    }

    private void SpawnCard(int cardValue, Cascade cascade)
    {
        var card = Card.InstantiateCard(cardValue, cascade);
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
