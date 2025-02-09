using UnityEngine;

public class VulnerablePlatform : Platform, IDamageable
{
    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            TakeDamage(Constants.BulletDamage);
            Destroy(bullet.gameObject);
        }
    }

    protected override void SetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(Constants.VulnerablePlatformLayerName);
    }
}

