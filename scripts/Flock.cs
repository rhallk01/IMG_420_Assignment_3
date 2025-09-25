using Godot;
using System;

public partial class Flock : Node2D
{
	[Export] public int NumberOfBoids = 10;
	[Export] public PackedScene BoidScene;
	

	public override void _Ready()
	{
		if (BoidScene == null)
		{
			GD.PrintErr("BoidScene not set.");
			return;
		}

		for (int i = 0; i < NumberOfBoids; i++)
		{
			SpawnBoid();
		}
	}

	private void SpawnBoid()
	{
		Boid boid = BoidScene.Instantiate<Boid>();
		boid.SetTarget(GetNode<RigidBody2D>("Target"));
		AddChild(boid);
	}
}
