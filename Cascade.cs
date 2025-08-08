using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Freecell;

public partial class Cascade : Stack
{
    // Mandatory properties.
    public override List<Card> CardsOnStack { get; set; } = [];
    public override Vector2 CardOffset { get; } = new(0, 17);

    public override bool CanAppendCard(int cardValue)
    {
        // If the stack is empty, we can append any card.
        if (CardsOnStack.Count <= 0)
            return true;

        var cardOnTop = CardsOnStack[^1];
        // The card should not be of the same color.
        if (Card.IsCardRed(cardValue) == Card.IsCardRed(cardOnTop.CardValue))
            return false;
        // The card value should be directly below the card on top.
        if (Card.GetCardNumber(cardValue) != Card.GetCardNumber(cardOnTop.CardValue) - 1)
            return false;
        return true;
    }

    // Local properties.
    private CollisionShape2D collider;
    private int cardsOnStackLastCount = 0;

    // Godot methods.
    private void _ready()
    {
        collider = GetNode<CollisionShape2D>($"./Collider");
    }

    private void _process(float _)
    {
        if (CardsOnStack.Count == cardsOnStackLastCount)
            return;
        cardsOnStackLastCount = CardsOnStack.Count;
        AdjustStackHeight();
    }

    // Local methods.
    private void AdjustStackHeight()
    {
        var shape = new RectangleShape2D();
        float absoluteStackedCards = Mathf.Max(CardsOnStack.Count - 1, 0);
        var height = 17;
        shape.Size = new Vector2(48, 64 + height * absoluteStackedCards);
        // Adjust collider position so that its top is aligned with the top of the stack.
        collider.Position = new Vector2(0, 8.5f * absoluteStackedCards);
        collider.Shape = shape;
    }
}
