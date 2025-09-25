using Godot;
using System;

public partial class Target : RigidBody2D
{
	[Export] public float Speed = 50f;
	[Export] public float LeftLimit = 550f;
	[Export] public float RightLimit = 850f;
	
	private int _direction = 1;
	private Sprite2D _sprite;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GravityScale = 0f;
		Freeze = true;		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		float newX = GlobalPosition.X + _direction * Speed * (float)delta;

		if (newX >= RightLimit)
		{
			newX = RightLimit;
			_direction = -1;
		}
		else if (newX <= LeftLimit)
		{
			newX = LeftLimit;
			_direction = 1;
		}

		GlobalPosition = new Vector2(newX, GlobalPosition.Y);	
		if (_sprite != null && _direction != 0)
			_sprite.FlipH = _direction < 0;			
	}
}
