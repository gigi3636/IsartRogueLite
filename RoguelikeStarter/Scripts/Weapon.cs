using Godot;

/// <summary>
/// Fires bullets.
/// Add it as a child of the player (or anything else that shoots).
/// It manages the fire cooldown and creates the bullet, but does not add it to the scene — it raises ShotFired and lets Game put the bullet in the world.
/// </summary>
public partial class Weapon : Node2D
{
    [Export] private PackedScene BulletScene { get; set; }
    [Export] private float BulletSpeed { get; set; } = 500f;
    [Export] private float FireCooldown { get; set; } = 0.25f;

    /// <summary>
    /// How far in front of the weapon the bullet appears.
    /// </summary>
    [Export] private float BulletSpawnOffset { get; set; } = 12f;

    /// <summary>
    /// Raised when a bullet is fired.
    /// The bullet exists but is not in the scene yet.
    /// </summary>
    [Signal] public delegate void ShotFiredEventHandler(Bullet bullet, Vector2 globalPosition);

    private float _cooldownTimer;

    public override void _Process(double delta)
    {
        if (_cooldownTimer > 0f)
            _cooldownTimer -= (float)delta;
    }

    /// <summary>
    /// Fires a bullet in the given direction, unless the cooldown is still running or the direction is zero.
    /// </summary>
    public void TryFire(Vector2 direction)
    {
        if (_cooldownTimer > 0f || direction == Vector2.Zero) return;

        if (BulletScene == null)
        {
            GD.PushWarning("Weapon: BulletScene is not assigned");
            return;
        }

        var bullet = BulletScene.Instantiate<Bullet>();
        bullet.Launch(direction, BulletSpeed);
        _cooldownTimer = FireCooldown;

        EmitSignal(SignalName.ShotFired, bullet, GlobalPosition + direction * BulletSpawnOffset);
    }
}
