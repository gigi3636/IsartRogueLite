using Godot;

/// <summary>
/// Handles input, movement and the hit blink.
/// A child HealthComponent holds the health, and a child Weapon does the shooting.
/// </summary>
public partial class Player : CharacterBody2D, IDamageable
{
    [Export] private float Speed { get; set; } = 220f;

    [Export] private HealthComponent _health;
    [Export] private Weapon _weapon;
    [Export] private CanvasItem _sprite;

    private const float BlinkFrequency = 30f;
    private const float BlinkAlpha = 0.4f;

    /// <summary>
    /// Exposed so Game can connect the HUD to the health.
    /// </summary>
    public HealthComponent Health => _health;

    /// <summary>
    /// Exposed so Game can catch the bullets the player fires.
    /// </summary>
    public Weapon Weapon => _weapon;

    public override void _Ready()
    {
        _health.Died += OnDied;
    }

    public override void _PhysicsProcess(double delta)
    {
        // Movement (ZQSD / arrow keys are reserved for shooting)
        Vector2 input = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = input * Speed;
        MoveAndSlide();

        // Shooting in the 4 directions with the arrow keys (like Isaac)
        _weapon.TryFire(ReadShootDirection());

        UpdateBlink();
    }

    public void TakeDamage(int amount) => _health.TakeDamage(amount);

    private static Vector2 ReadShootDirection()
    {
        if (Input.IsActionPressed("shoot_up")) return Vector2.Up;
        if (Input.IsActionPressed("shoot_down")) return Vector2.Down;
        if (Input.IsActionPressed("shoot_left")) return Vector2.Left;
        if (Input.IsActionPressed("shoot_right")) return Vector2.Right;
        return Vector2.Zero;
    }

    private void UpdateBlink()
    {
        if (_sprite == null) return;

        if (_health.IsInvulnerable)
        {
            float alpha = Mathf.Sin(_health.InvulnerabilityRemaining * BlinkFrequency) > 0 ? BlinkAlpha : 1f;
            _sprite.Modulate = new Color(1, 1, 1, alpha);
        }
        else if (_sprite.Modulate.A < 1f)
        {
            _sprite.Modulate = Colors.White;
        }
    }

    private void OnDied()
    {
        // The game-over screen is up to you.
        SetPhysicsProcess(false);
        Hide();
    }
}
