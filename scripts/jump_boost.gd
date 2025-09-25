extends Area2D

signal boost
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	set_deferred("monitoring", true)
	

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	

func _on_body_entered(body: Node2D) -> void:
	if body.is_in_group("Player"):
		boost.emit()
