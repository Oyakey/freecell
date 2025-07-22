using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Freecell;

public partial class Freecell : Stack
{
	// Mandatory properties.
	public override List<Card> CardsOnStack { get; set; } = [];
	public override string ObjectType { get; } = "FREECELL";
	public override Vector2 CardOffset { get; } = new(0, 0);

	public override bool CanAppendCard(int cardValue)
	{
		return CardsOnStack.Count <= 0;
	}
}
