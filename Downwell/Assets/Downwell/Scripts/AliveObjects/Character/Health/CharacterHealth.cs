using R3;
using System.Collections;
using UnityEngine;

public class CharacterHealth : AliveObject, IHealable
{
    [Header("Invulnerability Duration")]
    [SerializeField, Range(0.5f, 5f)] private float _invulnerabilityDuration;

    private const int DefaultMaxHPCount = 4;
    private bool _isInvulnerable;
    private int _currentMaxHealth;
    private HealthView _healthView;

    public override void TakeDamage(int damage)
    {
        if (_isInvulnerable)
            return;

        base.TakeDamage(damage);
        ActivateInvulnerability(_invulnerabilityDuration);
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

    public void ActivateInvulnerability(float duration)
    {
        if (!_isInvulnerable)
            StartCoroutine(InvulnerabilityCoroutine(duration));
    }

    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        _isInvulnerable = false;
    }
}
