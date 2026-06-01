/// <summary>
/// Implemented by anything that can take damage.
/// Bullets and enemies call TakeDamage through this interface, so they don't need to know what they actually hit.
/// </summary>
public interface IDamageable
{
    void TakeDamage(int amount);
}
