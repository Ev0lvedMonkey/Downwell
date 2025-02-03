﻿public interface IAlive
{
    public const int MaxHealthPoint = 10;
    public const int MinHealthPoint = 0;

    public void TakeDamage(int damage);
    public void TakeHeal(int healthPoint);
    public void Die();
}