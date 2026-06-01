using Godot;

/// <summary>
/// Tracks health and the brief invulnerability after a hit.
/// Add it as a child of the player or an enemy.
/// Raises HealthChanged whenever the health changes, and Died at zero.
/// </summary>
public partial class HealthComponent : Node
{
    [Export] private int MaxHealth { get; set; } = 3;

    /// <summary>
    /// How long the entity stays invulnerable after a hit.
    /// 0 = no invulnerability (enemies, for example).
    /// </summary>
    [Export] private float DamageCooldown { get; set; } = 0f;

    [Signal] public delegate void HealthChangedEventHandler(int current, int max);
    [Signal] public delegate void DiedEventHandler();

    private int _current;
    private float _invulnerabilityTimer;

    /// <summary>
    /// Current health points.
    /// </summary>
    public int Current => _current;

    /// <summary>
    /// Maximum health points.
    /// </summary>
    public int Max => MaxHealth;

    /// <summary>
    /// True right after a hit, while damage is ignored.
    /// </summary>
    public bool IsInvulnerable => _invulnerabilityTimer > 0f;

    /// <summary>
    /// Invulnerability time left. Used to drive the blink.
    /// </summary>
    public float InvulnerabilityRemaining => _invulnerabilityTimer;

    public override void _Ready()
    {
        _current = MaxHealth;
        EmitSignal(SignalName.HealthChanged, _current, MaxHealth);
    }

    public override void _Process(double delta)
    {
        if (_invulnerabilityTimer > 0f)
            _invulnerabilityTimer -= (float)delta;
    }

    /// <summary>
    /// Removes health points.
    /// Does nothing while invulnerable.
    /// Raises HealthChanged, and Died at zero.
    /// </summary>
    public void TakeDamage(int amount)
    {
        if (IsInvulnerable || _current <= 0) return;

        _current -= amount;
        _invulnerabilityTimer = DamageCooldown;
        EmitSignal(SignalName.HealthChanged, _current, MaxHealth);

        if (_current <= 0)
            EmitSignal(SignalName.Died);
    }
}
