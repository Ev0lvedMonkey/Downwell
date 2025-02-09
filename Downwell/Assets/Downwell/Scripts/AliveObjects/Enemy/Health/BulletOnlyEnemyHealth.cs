using UnityEngine;

public class BulletOnlyEnemyHealth : EnemyHealth
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollisionDamage(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollisionDamage(collision);
    }

    private void HandleCollisionDamage(Collision2D collision)
    {
        Transform collisionTransform = collision.transform;
        if (collisionTransform.TryGetComponent(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            TakeDamage(bullet.GetDamage());
            return;
        }

        if (collisionTransform.TryGetComponent(out CharacterHealth character))
        {
            ContactPoint2D contact = collision.contacts[0];
            float normalY = contact.normal.y;

            if (normalY > -0.5f)
            {
                Debug.LogWarning($"Снизу");
                if (character != null)
                    character.TakeDamage(1);
            }
        }

    }
}
