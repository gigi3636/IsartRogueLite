using Godot;

/// <summary>
/// Sets up the game: spawns the room and the player, then connects them (player health to the HUD, the player's weapon to bullet spawning).
///
/// Dungeon generation and room transitions are up to you.
/// </summary>
public partial class Game : Node2D
{
    [Export] private PackedScene PlayerScene { get; set; }

    private Node2D _projectiles;

    public override void _Ready()
    {


        // After the room, so bullets draw above the floor instead of under it.
        _projectiles = new Node2D { Name = "Projectiles" };
        AddChild(_projectiles);

        var player = SpawnPlayer();
    }

    private Player SpawnPlayer()
    {
        var player = PlayerScene.Instantiate<Player>();

        // Connect the signals before the player enters the tree.
        var hud = GetNodeOrNull<HUD>("HUD");
        if (hud != null)
            player.Health.HealthChanged += hud.OnPlayerHealthChanged;

        player.Weapon.ShotFired += OnShotFired;

        AddChild(player);
        player.AddToGroup("player"); // used by doors to detect the player
        return player;
    }

    private void OnShotFired(Bullet bullet, Vector2 globalPosition)
    {
        _projectiles.AddChild(bullet);
        bullet.GlobalPosition = globalPosition;
    }
}
