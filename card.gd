extends Area2D

class_name Card

@onready var _animated_sprite := $AnimatedSprite2D
@export var cardValue := 0
@export var stack: Area2D = null
@export var order := 0
var collidingStacks := [];
var snapping := false;
var dragged := false;
const objectType := "CARD";

static var cardCountByColor := 13

static func getCardNumber(value: int) -> int:
	return value % cardCountByColor
	
static func getCardColor(value: int) -> int:
	return floor(value / float(cardCountByColor))

static func isCardRed(value: int) -> bool:
	# Red colors are 0, 1; Black colors are 2, 3.
	return getCardColor(value) == 0 or getCardColor(value) == 1

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_animated_sprite.set_frame_and_progress(cardValue, 0.0)
	z_index = order

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if (dragged):
		dragged = false;
		snapToStack();
	pass

func snapToPos(pos: Vector2):
	var tween = get_tree().create_tween()
	tween.tween_property($".", "position", Vector2(pos.x, pos.y), .15);

func snapToStack():
	if (stack == null):
		return;
	snapToPos(stack.position + stack.cardOffset * order);

func teleportToStack():
	if (stack == null):
		return;
	position = stack.position + stack.cardOffset * order;

func _on_area_entered(area: Area2D) -> void:
	if (
		"objectType" in area and (
			area.objectType == "STACK" or 
			area.objectType == "FREECELL" or 
			area.objectType == "FOUNDATION"
		)
	):
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
	dragged = true;
	var newStack = getClosestStack();
#
	# Check if card can be added to stack.
	if (newStack == null): return;
	if (!newStack.canAppendCard(cardValue)): return;
#
	# Proceed to add card to stack.
	if (stack != null):
		stack.cardsOnStack.erase($".");
	stack = newStack;
	order = newStack.cardsOnStack.size();
	newStack.cardsOnStack.append($".");

func canMoveCard():
	if (stack.objectType == "STACK" and stack.cardsOnStack.size() > order + 1):
		return false;
	return true;
