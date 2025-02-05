using UnityEngine;

public abstract class EnemyHealth : AliveObject
{
    [SerializeField, Range(1, 5)] private int _defaultHealth;

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        SetDefaultHealth(_defaultHealth);
        gameObject.layer = LayerMask.NameToLayer(Constants.EnemyLayerName);
        base.Init();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}

