using UnityEngine;

public class UniversalEnemyHealth : EnemyHealth
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform collisionTransform = collision.transform;
        if (collisionTransform.TryGetComponent(out CharacterHealth character)
            || collisionTransform.TryGetComponent(out Bullet bullet))
            TakeDamage(Constants.BulletDamage);
    }
}

