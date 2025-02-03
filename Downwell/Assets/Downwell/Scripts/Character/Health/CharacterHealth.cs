using R3;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IAlive
{
    public readonly ReactiveProperty<int> AliveObjectHealthPoint = new();
    public ReadOnlyReactiveProperty<bool> AliveObjectDie;
    private const int DefaultMaxHPCount = 4;

    private int _currentMaxHealth;
    private DisposableBag _disposables = new();
    private HealthView _healthView;

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(1);
        if (Input.GetKeyDown(KeyCode.H)) TakeHeal(1);
        if (Input.GetKeyDown(KeyCode.U)) HealthUp();
    }

    public void TakeDamage(int damage)
    {
        AliveObjectHealthPoint.Value -= damage;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);
    }

    public void TakeHeal(int healthPoint)
    {
        AliveObjectHealthPoint.Value += healthPoint;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} bro died");
    }

    public void HealthUp()
    {
        _currentMaxHealth++;
        if (_currentMaxHealth > IAlive.MaxHealthPoint)
            return;
        _healthView.SetMaxHealth(_currentMaxHealth);
        TakeHeal(1);
    }

    public void Init()
    {
        _healthView = ServiceLocator.Current.Get<HealthView>();
        _currentMaxHealth = DefaultMaxHPCount;
        AliveObjectHealthPoint.Value = _currentMaxHealth;
        _healthView.SetMaxHealth(_currentMaxHealth);
        AliveObjectDie = AliveObjectHealthPoint.Select(amount => amount <= 0).ToReadOnlyReactiveProperty();
        AliveObjectDie.Where(isDead => isDead).Subscribe(_ => Die()).AddTo(ref _disposables);
        AliveObjectHealthPoint.Subscribe(_healthView.UpdateUI);
        AliveObjectHealthPoint.Subscribe(_ =>
        Debug.Log($"{gameObject.name} changed HP. Now: {AliveObjectHealthPoint.Value} HP"));
    }
}
