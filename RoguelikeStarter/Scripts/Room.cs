using Godot;
using System.Collections.Generic;

/// <summary>
/// A room.
/// It holds its type (start, end, normal...), its size in tiles and its doors.
/// Building the dungeon and moving between rooms is up to you.
/// </summary>
public partial class Room : Node2D
{
    public enum RoomType { Start, End, Normal, Challenge, Hidden, Reward }

    [Export] public RoomType Type { get; private set; } = RoomType.Normal;

    [Export] private TileMap _tileMap;
    [Export] private Camera2D _camera;
    [Export] private Node2D _enemies;
    private Dictionary<Door.Side, Door> _doors = new();

    public override void _Ready()
    {
        if (_tileMap != null)
        {
            ConfigureCamera();
        }

        // Find every door that belongs to this room.
        foreach (var node in GetTree().GetNodesInGroup("doors"))
        {
            if (node is Door d && IsAncestorOf(d))
            {
                _doors[d.Direction] = d;
            }
        }
    }

    /// <summary>
    /// Called when the player enters this room.
    /// Activates the camera and gives the player to the enemies so they can chase it.
    /// </summary>
    public void Enter(Player player)
    {
        Activate();
        GiveTargetToEnemies(player);
    }

    /// <summary>
    /// Turns on this room's camera.
    /// </summary>
    private void Activate()
    {
        _camera?.MakeCurrent();
    }

    private void GiveTargetToEnemies(Node2D target)
    {
        if (_enemies == null) return;

        foreach (var child in _enemies.GetChildren())
            if (child is Enemy enemy)
                enemy.Initialize(target);
    }

    /// <summary>
    /// Returns the door on the given side, or null if there is none.
    /// </summary>
    public Door GetDoor(Door.Side side)
    {
        _doors.TryGetValue(side, out var d);
        return d;
    }

    /// <summary>
    /// All the doors of the room.
    /// </summary>
    public IReadOnlyDictionary<Door.Side, Door> Doors => _doors;

    /// <summary>
    /// Size of the room in tiles, read from the TileMap.
    /// </summary>
    public Vector2I SizeInTiles
    {
        get
        {
            if (_tileMap == null) return Vector2I.Zero;
            return _tileMap.GetUsedRect().Size;
        }
    }

    private void ConfigureCamera()
    {
        if (_camera == null) return;
        Rect2I rect = _tileMap.GetUsedRect();
        Vector2I cell = _tileMap.TileSet?.TileSize ?? new Vector2I(16, 16);
        Vector2 topLeft = _tileMap.MapToLocal(rect.Position) - (Vector2)cell / 2f;
        Vector2 bottomRight = _tileMap.MapToLocal(rect.End) - (Vector2)cell / 2f;
        Vector2 worldTL = _tileMap.ToGlobal(topLeft);
        Vector2 worldBR = _tileMap.ToGlobal(bottomRight);

        _camera.LimitLeft = (int)worldTL.X;
        _camera.LimitTop = (int)worldTL.Y;
        _camera.LimitRight = (int)worldBR.X;
        _camera.LimitBottom = (int)worldBR.Y;
        _camera.LimitSmoothed = false;
    }
}
