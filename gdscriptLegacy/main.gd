extends Node2D
var cardScene = preload("res://card.tscn")
@onready var stacks := [
	$"Stack",
	$"Stack2",
	$"Stack3",
	$"Stack4",
	$"Stack5",
	$"Stack6",
	$"Stack7",
	$"Stack8",
];

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var cardValues := range(13*4);
	cardValues.shuffle();
	for stack in range(stacks.size()):
		var cardsOnStackCount = 7 if stack < 4 else 6;
		for i in range(cardsOnStackCount):
			spawnCard(cardValues[0], stacks[stack], i)
			cardValues.pop_at(0);

func spawnCard(cardValue: int, stack: Area2D, order: int):
	var card := cardScene.instantiate();
	card.cardValue = cardValue;
	card.stack = stack;
	# card.order = order;
	card.order = stack.cardsOnStack.size()
	stack.cardsOnStack.append(card)
	card.teleportToStack();
	add_child(card);
