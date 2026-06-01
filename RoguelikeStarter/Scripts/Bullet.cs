using Godot;

/// <summary>
/// Moves in a straight line and damages whatever it hits, if that thing implements IDamageable.
/// It dies on any collision.
/// What it can actually collide with is set by the collision layers in Bullet.tscn, not here.
/// </summary>
public partial class Bullet : Area2D
{
    [Export] private float Speed { get; set; } = 500f;
    [Export] private float Lifetime { get; set; } = 1.5f;
    [Export] private int Damage { get; set; } = 1;

    private Vector2 _direction = Vector2.Right;
    private float _life;

    public override void _Ready()
    {
        _life = Lifetime;
        BodyEntered += OnBodyEntered;
    }

    /// <summary>
    /// Sets direction and speed before the bullet is added to the scene.
    /// </summary>
    public void Launch(Vector2 direction, float speed)
    {
        _direction = direction;
        Speed = speed;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += _direction * Speed * (float)delta;
        _life -= (float)delta;
        if (_life <= 0f) QueueFree();
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is IDamageable damageable)
            damageable.TakeDamage(Damage);

        QueueFree();
    }
}
