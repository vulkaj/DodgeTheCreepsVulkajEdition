using Godot;

public partial class HUD : CanvasLayer
{
	// Don't forget to rebuild the project so the editor knows about the new signal.

	[Signal]
	public delegate void StartGameEventHandler();
	
	
	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();

		GetNode<Timer>("MessageTimer").Start();
	}
	
	async public void ShowGameOver()
	{
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the\nCreeps!";
		message.Show();

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}
	
	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
	public void ActualizaRecord(int record, bool mostrar)
	{	
		GetNode<Label>("Record").Text = "Record" + " " + record.ToString();
		
		if (mostrar){
			GetNode<Label>("Record").Show();
		} else {
			GetNode<Label>("Record").Hide();
		}
	}
	
	
	private void _on_message_timer_timeout()
	{
		  GetNode<Label>("Message").Hide();
	}
	
	
	private void _on_start_button_pressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
	}
	
}






