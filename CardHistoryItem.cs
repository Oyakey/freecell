namespace Freecell;

public class CardHistoryItem(Card card, Stack previousStack, Stack newStack) : IHistoryItem
{
    private readonly Card _card = card;
    private readonly Stack _previousStack = previousStack;
    private readonly Stack _newStack = newStack;

    public void Forward()
    {
        _card.MoveToStack(_newStack);
    }

    public void Revert()
    {
        _card.MoveToStack(_previousStack);
    }
}
