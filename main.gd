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
	var cards = [];
	for i in range(13*4):
		cards.append(i)
	cards.shuffle();
	var index = 0;
	for stack in stacks:
		var cardsForStack = cards.slice(0,7);
		for i in range(cardsForStack.size()):
			var card = spawnCard(cardsForStack[i],stack,i);
			stack.cardsOnStack.append(card);
		index+=1;


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func spawnCard(cardValue: int, stack: Area2D, order: int):
	var card := cardScene.instantiate();
	card.cardValue = cardValue;
	card.stack = stack;
	card.order = order;
	add_child(card);
	return card;
