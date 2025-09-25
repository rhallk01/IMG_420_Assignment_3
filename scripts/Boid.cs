using Godot;
using System.Collections.Generic;

public partial class Boid : CharacterBody2D
{
	// Adjustable in the Inspector
	// The settings initially keep the bee boids in a more bee like formation
	// Slowed down max speed
	[Export] public float MaxSpeed = 80f;
	// Increased seperation weight
	[Export] public float SeparationWeight = 5.0f;
	[Export] public float AlignmentWeight = 1.2f;
	// Increased cohesion weight
	[Export] public float CohesionWeight = 2.5f;
	// Increased follow weight and radius significantly
	[Export] public float FollowWeight = 70.0f;
	[Export] public float FollowRadius = 60.0f;
	private List<Boid> _neighbors = new();
	private Area2D _detectionArea;
	private RigidBody2D _target;
	private Sprite2D _sprite;
	
	public override void _Ready()
	{
		_detectionArea = GetNode<Area2D>("DetectionArea");
		_detectionArea.BodyEntered += OnBodyEntered;
		_detectionArea.BodyExited += OnBodyExited;
		// made the bee boids into sprites
		_sprite = GetNode<Sprite2D>("Sprite2D");
		
		var viewportRect = GetViewportRect();
		var randomX = GD.Randf() * viewportRect.Size.X;
		var randomY = GD.Randf() * viewportRect.Size.Y/3;
		Position = new Vector2(randomX, randomY);
		
		var randomAngle = GD.Randf() * Mathf.Pi * 2;
		var randomVelocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * MaxSpeed;
		Velocity = randomVelocity;
		MoveAndSlide();
		
		// Optional: Make boid face its direction of movement
		if (Velocity.X != 0)
			_sprite.FlipH = Velocity.X < 0;
		LookAt(Position + Velocity);
	}
	public void SetTarget(RigidBody2D Target)
	{
		_target = Target;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 separationVector = Separation() * SeparationWeight;
		
		Vector2 alignmentVector = Alignment() * AlignmentWeight;
		Vector2 cohesionVector = Cohesion() * CohesionWeight;
		Vector2 followVector = Centralization() * FollowWeight;
		//GD.Print($"The value of my variable is: {followVector}");
		Velocity += (separationVector + alignmentVector + cohesionVector + followVector) * (float)delta;
		
		// Clamp velocity to prevent boids from moving too fast
		Velocity = Velocity.LimitLength(MaxSpeed);
		//GD.Print($"The value of my variable is: {Velocity}");
		MoveAndSlide();
		
		// Optional: Make boid face its direction of movement
		LookAt(Position + Velocity);
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Boid boid && body != this)
		{
			_neighbors.Add(boid);
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Boid boid && body != this)
		{
			_neighbors.Remove(boid);
		}
	}

	// Rule 1: Separation—Avoid crowding neighbors
	private Vector2 Separation()
	{
		if (_neighbors.Count == 0) return Vector2.Zero;
        // used desired seperation to adjust bees distance from each other
		float desiredSeparation = 40f; 
		Vector2 steer = Vector2.Zero;
		foreach (var neighbor in _neighbors)
		{
			float d = Position.DistanceTo(neighbor.Position);
			// steers bees back towards desired seperation
			if (d < desiredSeparation && d > 0f)
			{
				Vector2 diff = (Position - neighbor.Position).Normalized();
				// stronger only when very close, weaker when farther
				steer += diff * (1f - d / desiredSeparation);
			}
		}
		return steer.Normalized();
	}

	// Rule 2: Alignment—Steer towards the average heading of neighbors
	private Vector2 Alignment()
	{
		if (_neighbors.Count == 0) return Vector2.Zero;

		Vector2 averageVelocity = Vector2.Zero;
		foreach (var neighbor in _neighbors)
		{
			averageVelocity += neighbor.Velocity;
		}
		averageVelocity /= _neighbors.Count;
		return averageVelocity.Normalized();
	}

	// Rule 3: Cohesion—Move towards the average position of neighbors
	private Vector2 Cohesion()
	{
		if (_neighbors.Count == 0) return Vector2.Zero;

		Vector2 centerOfMass = Vector2.Zero;
		foreach (var neighbor in _neighbors)
		{
			centerOfMass += neighbor.Position;
		}
		centerOfMass /= _neighbors.Count;

		Vector2 directionToCenter = centerOfMass - Position;
		return directionToCenter.Normalized();
	}
	
	private Vector2 Centralization()
	{
		if(_target != null)
		{
			////GD.Print($"The value of my variable is: {_target.GetPosition()}");
			////GD.Print($"The value of my variable is: {Position}");
			if (Position.DistanceTo(_target.GetPosition()) < FollowRadius)
			{
				return Vector2.Zero;
			}
			else
			{
				return ((_target.GetPosition() - Position).Normalized());
			}
		}
		else
		{
			return Vector2.Zero;
		}
		
	}
	private void BoidExitedScreen()
	{
		//GD.Print("Got here");
		Velocity *= -1;
		MoveAndSlide();
		
		// Optional: Make boid face its direction of movement
		LookAt(Position + Velocity);
	}
}
