public interface IAlive : IDamageable
{
    public const int MaxHealthPoint = 10;
    public const int MinHealthPoint = 0;
    public void Die();
}