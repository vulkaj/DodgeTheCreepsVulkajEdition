using Godot;
using System;

public partial class Patata : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var Sprite2D = GetNode<Sprite2D>("Sprite2D");
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}
	
	//Mostramos la patata
	public void Start()
	{
		
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	
	
	private void _on_area_entered(Area2D area)
	{
		Hide();
	}
}



