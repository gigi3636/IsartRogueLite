using Godot;

/// <summary>
/// Shows the current and max health.
/// Connect it to the player's HealthChanged signal.
/// </summary>
public partial class HUD : CanvasLayer
{
    [Export] private Label _healthLabel;

    public override void _Ready()
    {
        UpdateHealth(0, 0);
    }

    public void OnPlayerHealthChanged(int current, int max)
    {
        UpdateHealth(current, max);
    }

    private void UpdateHealth(int current, int max)
    {
        if (_healthLabel != null)
            _healthLabel.Text = $"♥ {current} / {max}";
    }
}
