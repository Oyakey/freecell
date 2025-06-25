extends Area2D

# Mandatory properties.
const objectType = "FOUNDATION";
const cardOffset := Vector2(0, 0);
var cardsOnStack := [];

# Local properties.
@export var color: int = 0;

func canAppendCard(cardValue: int):
	if (Card.getCardColor(cardValue) != color): return false;
	if (Card.getCardNumber(cardValue) > cardsOnStack.size()): return false;
	return true;
