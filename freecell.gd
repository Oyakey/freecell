extends Area2D

# Mandatory properties.
const objectType = "FREECELL";
const cardOffset := Vector2(0, 0);
var cardsOnStack := [];

func CanAppendCard(_cardValue: int):
	return cardsOnStack.size() <= 0;
