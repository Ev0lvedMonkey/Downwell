using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float _speed;
    protected Vector3 _direction;
    protected float _bulletLifeTime;
    protected float _bulletDamage;

    protected const int MinBulletsSpeed = 1;
    protected const int MaxBulletsSpeed = 100;

    public virtual void Init(float speed,float bulletDamage, float bulletLifeTime, Vector3 direction)
    {
        _speed = Mathf.Clamp(speed, MinBulletsSpeed, MaxBulletsSpeed);
        _bulletDamage = bulletDamage;
        _direction = direction;
        _bulletLifeTime = bulletLifeTime;
    }

    public float GetDamage()
    {
        return _bulletDamage;
    }
}