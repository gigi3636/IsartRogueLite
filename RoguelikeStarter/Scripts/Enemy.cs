using Godot;

/// <summary>
/// Walks straight at its target and damages it on contact.
/// A child HealthComponent handles its health, and implementing IDamageable lets bullets hurt it.
/// </summary>
public partial class Enemy : CharacterBody2D, IDamageable
{
    [Export] private float Speed { get; set; } = 90f;
    [Export] private int ContactDamage { get; set; } = 1;

    [Export] private HealthComponent _health;

    private Node2D _target;

    public override void _Ready()
    {
        _health.Died += OnDied;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_target == null || !IsInstanceValid(_target))
        {
            Velocity = Vector2.Zero;
            return;
        }

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
