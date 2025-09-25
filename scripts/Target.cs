using Godot;
using System;

public partial class Target : RigidBody2D
{
	// can be adjusted in editor
	// add variables for how far left and right the queen bee goes
	// set the speed of the queen bee
	[Export] public float Speed = 50f;
	[Export] public float LeftLimit = 550f;
	[Export] public float RightLimit = 850f;

	// set direction
	private int _direction = 1;
	// set sprite
	private Sprite2D _sprite;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//turn off gravity, so that she flies back and forth without changing y position
		GravityScale = 0f;
		Freeze = true;		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
	    //set new x position for queen bee
		float newX = GlobalPosition.X + _direction * Speed * (float)delta;
		
		//if at right limit, turn left
		if (newX >= RightLimit)
		{
			newX = RightLimit;
			_direction = -1;
		}
		//if at left limit, turn right
		else if (newX <= LeftLimit)
		{
			newX = LeftLimit;
			_direction = 1;
		}
		//set new position
		GlobalPosition = new Vector2(newX, GlobalPosition.Y);	
		//make bee look in the direction its going
		if (_sprite != null && _direction != 0)
			_sprite.FlipH = _direction < 0;			
	}
}
