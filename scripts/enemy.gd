extends Area2D

signal diedByEnemy

@onready var ap = $AnimationPlayerEnemy
const SPEED = 20
const far_left = 647
const far_right = 710
var direction = 1

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	ap.play("walk")
	position.x = 680
	if position.x >= far_right:
		direction = -1
		$Sprite2D.flip_h = true
	if position.x <= far_left:
		direction = 1
		$Sprite2D.flip_h = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if position.x >= far_right:
		direction = -1
		$Sprite2D.flip_h = true
	if position.x <= far_left:
		direction = 1
		$Sprite2D.flip_h = false

	position.x += direction * SPEED * delta


func _on_body_entered(body: Node2D) -> void:	
	if body.is_in_group("Player"):
		diedByEnemy.emit()
