extends Area2D

@onready var _animated_sprite = $AnimatedSprite2D
@export var cardValue = 0
@export var stack: Area2D = null
@export var order = 0
var snapping = false;
var dragged = false;
const objectType = "CARD";

func getCardColor() -> int:
	return cardValue % 13

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_animated_sprite.set_frame_and_progress(cardValue, 0.0)
	z_index = order

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if (dragged):
		dragged = false;
		if (stack != null):
			snapToPos(stack.position);
	pass

func snapToPos(position: Vector2):
	var tween = get_tree().create_tween()
	tween.tween_property($".", "position", Vector2(position.x, position.y), .15);

func _on_area_entered(area: Area2D) -> void:
	if ("objectType" in area and area.objectType == "STACK"):
		stack = area;
	
func _on_area_exited(area: Area2D) -> void:
	# stack = null;
	pass;
