using R3;
using UnityEngine;

public class CharacterHealth : AliveObject, IHealable
{
    private const int DefaultMaxHPCount = 4;

    private int _currentMaxHealth;
    private HealthView _healthView;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(1);
        if (Input.GetKeyDown(KeyCode.H)) TakeHeal(1);
        if (Input.GetKeyDown(KeyCode.U)) HealthUp();
    }

    public void TakeHeal(int healthPoint)
    {
        AliveObjectHealthPoint.Value += healthPoint;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);
    }

    public void HealthUp()
    {
        _currentMaxHealth++;
        if (_currentMaxHealth > IAlive.MaxHealthPoint)
            return;
        _healthView.SetMaxHealth(_currentMaxHealth);
        TakeHeal(1);
    }

    public override void Init()
    {
        _healthView = ServiceLocator.Current.Get<HealthView>();
        _currentMaxHealth = DefaultMaxHPCount;
        SetDefaultHealth(_currentMaxHealth);
        _healthView.SetMaxHealth(_currentMaxHealth);
        AliveObjectHealthPoint.Subscribe(_healthView.UpdateUI).AddTo(ref _disposables);
        base.Init();
    }
}
