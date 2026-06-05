using Godot;

/// <summary>
/// Walks straight at its target and damages it on contact.
/// A child HealthComponent handles its health, and implementing IDamageable lets bullets hurt it.
/// </summary>
public partial class Enemy : CharacterBody2D, IDamageable
{
    [Export] private float Speed { get; set; } = 90f;
    [Export] private Texture2D[] skins;
    [Export] private Sprite2D skin;
    [Export] private int ContactDamage { get; set; } = 1;

    [Export] private HealthComponent _health;

    private Node2D _target;
    private RandomNumberGenerator _randomNumberGenerator;

    public override void _Ready()
    {
        _health.Died += OnDied;
        _randomNumberGenerator = new RandomNumberGenerator();
        skin.Texture = skins[_randomNumberGenerator.RandiRange(0, skins.Length)];
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_target == null || !IsInstanceValid(_target))
        {
            Velocity = Vector2.Zero;
            Scale = Vector2.Zero;
            return;
        }
        Scale = new Vector2(3,3);

        Vector2 dir = (_target.GlobalPosition - GlobalPosition).Normalized();
        Velocity = dir * Speed;

        var collision = MoveAndCollide(Velocity * (float)delta);
        if (collision?.GetCollider() is IDamageable damageable)
            damageable.TakeDamage(ContactDamage);
    }

    /// <summary>
    /// Sets the target to chase.
    /// Call it before adding the enemy to the scene.
    /// </summary>
    public void Initialize(Node2D target) => _target = target;

    public void TakeDamage(int amount) => _health.TakeDamage(amount);

    private void OnDied() => QueueFree();
}
