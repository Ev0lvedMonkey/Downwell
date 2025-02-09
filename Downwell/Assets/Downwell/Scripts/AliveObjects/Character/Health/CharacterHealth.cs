using R3;
using System.Collections;
using UnityEngine;

public class CharacterHealth : AliveObject, IHealable
{
    [Header("Invulnerability Duration")]
    [SerializeField, Range(0.5f, 5f)] private float _baseInvulnerabilityDuration;

    private const int DefaultMaxHPCount = 4;
    private bool _isInvulnerable;
    private int _currentMaxHealth;
    private float _currentInvulnerabilityDuration;
    private HealthView _healthView;

    public override void TakeDamage(float damage)
    {
        if (_isInvulnerable)
            return;

        base.TakeDamage(damage);
        ActivateInvulnerability();
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
        _currentInvulnerabilityDuration = _baseInvulnerabilityDuration;
        SetDefaultHealth(_currentMaxHealth);
        _healthView.SetMaxHealth(_currentMaxHealth);
        AliveObjectHealthPoint.Subscribe(_healthView.UpdateUI).AddTo(ref _disposables);
        base.Init();
    }

    public void ActivateInvulnerability()
    {
        if (!_isInvulnerable)
            StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(_currentInvulnerabilityDuration);
        _isInvulnerable = false;
    }

    public void IncreaseInvulnerabilityTime(float amount)
    {
        _currentInvulnerabilityDuration += amount;
        Debug.Log($"[CharacterHealth] Invulnerability time increased by {amount}. New duration: {_currentInvulnerabilityDuration}");
    }

    public void ResetInvulnerabilityTime()
    {
        _currentInvulnerabilityDuration = _baseInvulnerabilityDuration;
        Debug.Log("[CharacterHealth] Invulnerability time reset to default.");
    }
}
