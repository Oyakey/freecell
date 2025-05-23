extends Area2D

@onready var _animated_sprite = $AnimatedSprite2D
@export var cardValue = 0
@export var stack: Area2D = null
@export var order := 0
var collidingStacks = [];
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
			snapToPos(stack.position + Vector2(0, order * 17.));
	pass

func snapToPos(pos: Vector2):
	var tween = get_tree().create_tween()
	tween.tween_property($".", "position", Vector2(pos.x, pos.y), .15);

func _on_area_entered(area: Area2D) -> void:
	if ("objectType" in area and area.objectType == "STACK"):
		collidingStacks.append(area);
	
func _on_area_exited(area: Area2D) -> void:
	collidingStacks.erase(area);
	pass;

func getClosestStack() -> Area2D:
	var closestStack: Area2D = null;
	for collidingStack in collidingStacks:
		if (closestStack == null):
			closestStack = collidingStack;
			continue;
		var collidingStackDist = (collidingStack.position - position)
		var closestDist = (closestStack.position - position)
		if (collidingStackDist.length() <= closestDist.length()):
			closestStack = collidingStack;
	return closestStack;

func addToStack() -> void:
	# dragging.z_index = dragging.order;
	dragged = true;
	var newStack = getClosestStack();
	if (newStack != null):
		if (stack != null):
			stack.cardsOnStack.erase($".")
		stack = newStack
		order = newStack.cardsOnStack.size()
		newStack.cardsOnStack.append($".")
