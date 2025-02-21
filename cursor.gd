extends Area2D

func _process(delta: float) -> void:
	# follow mouse cursor.
	position = get_viewport().get_camera_2d().get_global_mouse_position()
	pass
