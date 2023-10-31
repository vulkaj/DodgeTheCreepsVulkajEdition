using Godot;
using System;

public partial class Main : Node
{
	// Don't forget to rebuild the project so the editor knows about the new export variable.

	[Export]
	public PackedScene MobScene { get; set; }
	
	private int _score;
	private int _record;
	
	private AudioStreamPlayer Music;
	
	
	
	
	public override void _Ready()
	{
		_record = 0;
	}
	
	private void NewGame()
	{
		var hud = GetNode<HUD>("HUD");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Ready!");
		
		Music = GetNode<AudioStreamPlayer>("Music");
		Music.Play(0);
		
		if (_score > _record){
			_record = _score;
		}
		
		_score = 0;
		
		if(_record == 0){
			hud.ActualizaRecord(_record, false);
		} else {
			hud.ActualizaRecord(_record, true);
		}

		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();
	}
	
	

	
	private void game_over()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<HUD>("HUD").ShowGameOver();
	}


//	public void NewGame()
//	{
//		_score = 0;
//
//		var player = GetNode<Player>("Player");
//		var startPosition = GetNode<Marker2D>("StartPosition");
//		player.Start(startPosition.Position);
//
//		GetNode<Timer>("StartTimer").Start();
//	}
	
	
	
	
	private void _on_mob_timer_timeout()
	{
		// Note: Normally it is best to use explicit types rather than the `var`
		// keyword. However, var is acceptable to use here because the types are
		// obviously Mob and PathFollow2D, since they appear later on the line.

		// Create a new instance of the Mob scene.
		Mob mob = MobScene.Instantiate<Mob>();
		
		// Choose a random location on Path2D.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mob.Position = mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);// Replace with function body.
	}
	
	
	private void _on_score_timer_timeout()
	{
		_score++;
		GetNode<HUD>("HUD").UpdateScore(_score);
	}


	private void _on_start_timer_timeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
	
	
}













