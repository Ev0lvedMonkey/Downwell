using UnityEngine;

public class ProjectileBullet : Bullet
{
    public override void Init(float speed, Vector3 direction)
    {
        base.Init(speed, direction);
        Destroy(gameObject, BulletLifeTime);
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}