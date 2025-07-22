using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Freecell;

public partial class Cascade : Area2D, IStack
{
    // Mandatory properties.
    public List<Card> CardsOnStack { get; set; } = [];
    public string ObjectType { get; } = "STACK";
    public Vector2 CardOffset { get; } = new(0, 17);

    public bool CanAppendCard(int cardValue)
    {
        var cardOnTop = CardsOnStack.Last();
        // If the stack is empty, we can append any card.
        if (cardOnTop == null) return true;
        // The card should not be of the same color.
        if (Card.IsCardRed(cardValue) == Card.IsCardRed(cardOnTop.CardValue)) return false;
        // The card value should be directly below the card on top.
        if (Card.GetCardNumber(cardValue) != Card.GetCardNumber(cardOnTop.CardValue) - 1) return false;
        return true;
    }

    // Local properties.
    private CollisionShape2D collider;
    private int cardsOnStackLastCount = 0;

    private void _ready()
    {
        collider = GetNode<CollisionShape2D>($"./Collider");
    }

    private void _process()
    {
        if (CardsOnStack.Count == cardsOnStackLastCount) return;
        cardsOnStackLastCount = CardsOnStack.Count;
        var shape = new RectangleShape2D();
        float absoluteStackedCards = Mathf.Max(CardsOnStack.Count - 1, 0);
        var height = 17;
        shape.Size = new Vector2(48, 64 + height * absoluteStackedCards);
        collider.Position = new Vector2(0, 8.5f * absoluteStackedCards);
        collider.Shape = shape;
    }
}
