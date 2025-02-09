using R3;
using UnityEngine;

public abstract class AliveObject : MonoBehaviour, IAlive
{
    public readonly ReactiveProperty<float> AliveObjectHealthPoint = new();
    public ReadOnlyReactiveProperty<bool> AliveObjectDie;

    protected DisposableBag _disposables = new();

    protected void OnDestroy()
    {
        _disposables.Dispose();
    }

    public virtual void TakeDamage(float damage)
    {
        AliveObjectHealthPoint.Value -= damage;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} bro died");
    }

    public virtual void Init()
    {
        AliveObjectDie = AliveObjectHealthPoint.Select(amount => amount <= 0).ToReadOnlyReactiveProperty();
        AliveObjectDie.Where(isDead => isDead).Subscribe(_ => Die()).AddTo(ref _disposables);
        AliveObjectHealthPoint.Subscribe(_ =>
        Debug.Log($"{gameObject.name} changed HP. Now: {AliveObjectHealthPoint.Value} HP")).AddTo(ref _disposables);
    }

    protected void SetDefaultHealth(int amount)
    {
        AliveObjectHealthPoint.Value = amount;
    }

}

