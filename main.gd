extends Node2D
var scene = preload("res://card.tscn")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var cardValues := range(13*4);
	var stacks = [
		$Stack,
		$Stack2,
		$Stack3,
		$Stack4,
		$Stack5,
		$Stack6,
		$Stack7,
		$Stack8
	];
	cardValues.shuffle();
	for stack in range(stacks.size()):
		var cardsOnStackCount = 7 if stack < 4 else 6;
		for i in range(cardsOnStackCount):
			spawnCard(cardValues[0], stacks[stack], i)
			cardValues.pop_at(0);
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func spawnCard(cardValue: int, stack: Area2D, order: int):
	var card := scene.instantiate();
	card.cardValue = cardValue;
	card.stack = stack;
	card.teleportToStack();
	card.order = order;
	add_child(card);
