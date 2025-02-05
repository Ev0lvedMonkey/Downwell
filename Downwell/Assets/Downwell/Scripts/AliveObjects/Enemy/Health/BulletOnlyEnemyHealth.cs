using UnityEngine;

public class BulletOnlyEnemyHealth : EnemyHealth
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamageToCharacter(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        DealDamageToCharacter(collision);
    }

    private void DealDamageToCharacter(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out CharacterHealth character))
            character.TakeDamage(1);
    }
}
