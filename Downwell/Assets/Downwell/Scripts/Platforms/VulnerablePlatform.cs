using UnityEngine;

public class VulnerablePlatform : Platform, IDamageable
{
    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }

    protected override void SetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(Constants.VulnerablePlatformLayerName);
    }
}

