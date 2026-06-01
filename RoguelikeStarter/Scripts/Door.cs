using Godot;

/// <summary>
/// A gap in the wall.
/// Raises PlayerCrossed, with its direction, when the player walks through.
/// Reacting to it (changing room) is up to you.
/// </summary>
public partial class Door : Area2D
{
    public enum Side { North, South, East, West }

    [Export] public Side Direction { get; private set; } = Side.North;

    [Signal] public delegate void PlayerCrossedEventHandler(Node2D player, int direction);

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
            EmitSignal(SignalName.PlayerCrossed, body, (int)Direction);
    }
}
