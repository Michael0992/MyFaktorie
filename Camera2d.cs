using Godot;
using System;

public partial class Camera2d : Camera2D
{
	private CharacterBody2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNodeOrNull<CharacterBody2D>("../Player/CharacterBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = player.GlobalPosition;
	}
}
