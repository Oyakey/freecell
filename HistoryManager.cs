using System.Collections.Generic;

namespace Freecell;

public class HistoryManager
{
    private static readonly List<IHistoryItem> _historyItems = [];

    public static bool CanUndo => _historyItems.Count > 0;

    public static void Push(IHistoryItem item)
    {
        _historyItems.Add(item);
    }

    public static void Undo()
    {
        if (_historyItems.Count == 0)
            return;

        _historyItems[^1].Revert();
        _historyItems.RemoveAt(_historyItems.Count - 1);
    }
}
