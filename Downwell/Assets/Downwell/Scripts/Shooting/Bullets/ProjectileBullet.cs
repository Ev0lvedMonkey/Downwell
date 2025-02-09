using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBullet : Bullet
{
    public override void Init(float speed, float bulletDamage, float bulletLifeTime, Vector3 direction)
    {
        base.Init(speed, bulletDamage, bulletLifeTime, direction);
        Destroy(gameObject, _bulletLifeTime);
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}