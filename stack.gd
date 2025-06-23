extends Area2D

const objectType = "STACK";
var cardsOnStack := [];
@onready var collider : CollisionShape2D = $"./Collider";
var cardsOnStackLastCount := 0;

func canAppendCard():
	# TODO: add rules for adding card to a stack (value -1 and opposite color).
	return true;
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if (cardsOnStack.size() != cardsOnStackLastCount):
		cardsOnStackLastCount = cardsOnStack.size();
		var shape = RectangleShape2D.new();
		var absoluteStackedCards : float = max(cardsOnStack.size() - 1,0.)
		var height := 17;
		shape.size = Vector2(48, 64 + height * absoluteStackedCards);
		collider.position = Vector2(0, 8.5 * absoluteStackedCards);
		collider.set_shape(shape);
