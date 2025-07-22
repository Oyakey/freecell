extends Area2D

# TODO: Could be refactored to a class / interface.
# Mandatory properties.
const objectType = "STACK";
const cardOffset := Vector2(0, 17.);
var cardsOnStack := [];

# Local properties.
@onready var collider : CollisionShape2D = $"./Collider";
var cardsOnStackLastCount := 0;

func CanAppendCard(cardValue: int):
	var cardOnTop = cardsOnStack.back()
	# If the stack is empty, we can append any card.
	if (cardOnTop == null): return true;
	# The card should not be of the same color.
	if (Card.isCardRed(cardValue) == Card.isCardRed(cardOnTop.cardValue)): return false;
	# The card value should be directly below the card on top.
	if (Card.getCardNumber(cardValue) != Card.getCardNumber(cardOnTop.cardValue) - 1): return false;
	return true;

func _process(_delta: float) -> void:
	if (cardsOnStack.size() != cardsOnStackLastCount):
		cardsOnStackLastCount = cardsOnStack.size();
		var shape = RectangleShape2D.new();
		var absoluteStackedCards : float = max(cardsOnStack.size() - 1,0.)
		var height := 17;
		shape.size = Vector2(48, 64 + height * absoluteStackedCards);
		collider.position = Vector2(0, 8.5 * absoluteStackedCards);
		collider.set_shape(shape);
