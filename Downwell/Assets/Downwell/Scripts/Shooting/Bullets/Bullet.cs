using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float _speed;
    protected Vector3 _direction;

    protected const int MinBulletsSpeed = 1;
    protected const int MaxBulletsSpeed = 100;
    protected const float BulletLifeTime = 2f;

    public virtual void Init(float speed, Vector3 direction)
    {
        _speed = Mathf.Clamp(speed, MinBulletsSpeed, MaxBulletsSpeed);
        _direction = direction;
    }
}