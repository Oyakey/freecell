namespace Freecell;

public interface IHistoryItem
{
    void Forward();
    void Revert();
}
