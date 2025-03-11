extends Node2D
var scene = preload("res://card.tscn")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	for i in range(13*4):
		spawnCard(i, null, i)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func spawnCard(cardValue: int, stack: Area2D, order: int):
	var card := scene.instantiate();
	card.cardValue = cardValue;
	card.stack = stack;
	card.order = order;
	add_child(card);
