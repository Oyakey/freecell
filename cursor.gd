extends Area2D

var collidingAreas := [];
var dragging: Area2D = null;
var zindex := 50;
var offset : Vector2 = Vector2.ZERO;

func _process(_delta: float) -> void:
	# follow mouse cursor.
	var mousePosition := get_viewport().get_camera_2d().get_global_mouse_position();
	position = mousePosition;

	var colliding = getFirstCollider();
	
	if dragging != null:
		if Input.is_action_pressed("leftMouse"):
			dragging.position = mousePosition + offset
		else:
			# dragging.z_index = dragging.order;
			dragging = null
	else:
		if colliding != null and Input.is_action_pressed("leftMouse"):
			dragging = colliding
			zindex += 1;
			dragging.z_index = zindex;
			offset = dragging.position - position

func _on_area_entered(area: Area2D) -> void:
	collidingAreas.append(area);
	
func _on_area_exited(area: Area2D) -> void:
	collidingAreas.erase(area);

func getFirstCollider() -> Area2D:
	var areaOnTop: Area2D = null;
	for area: Area2D in collidingAreas:
		if areaOnTop == null or areaOnTop.z_index < area.z_index :
			areaOnTop = area;
			continue;
		  # if area.
		pass
	return areaOnTop;
