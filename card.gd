extends Area2D

@onready var _animated_sprite = $AnimatedSprite2D
@export var cardValue = 0

func getCardColor() -> int:
	return cardValue % 13

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_animated_sprite.set_frame_and_progress(cardValue, 0.0)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	pass
