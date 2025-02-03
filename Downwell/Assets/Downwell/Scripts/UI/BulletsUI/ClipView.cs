using R3;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClipView : MonoBehaviour, IService
{
    [SerializeField] private Transform _bulletsHolder;
    [SerializeField] private BulletView _bulletPrefab;
    [SerializeField] private TextMeshProUGUI _bulletsText;

    private readonly ReactiveProperty<int> BulletsCount = new();
    private readonly List<BulletView> _bulletsList = new();
    private int _maxBulletsCount;
    private float _bulletHeight;
    private StreamBus _streamBus;

    private static readonly Dictionary<int, float> BulletHeights = new()
    {
        { 1, 845 }, { 2, 420 }, { 3, 280 }, { 4, 210 }, { 5, 167 },
        { 6, 137 }, { 7, 118 }, { 8, 103 }, { 9, 92 }, { 10, 82 },
    };

    public void Init()
    {
        _streamBus = ServiceLocator.Current.Get<StreamBus>();
        _streamBus.OnShotEvent.Subscribe(_ => ReduceBullet());
        _streamBus.OnFellToGroundEvent.Subscribe(_ => Reload());
        BulletsCount.Skip(1).Subscribe(_ => UpdateUI());
        BulletsCount.Skip(1).Subscribe(_ => _bulletsText.text = $"{BulletsCount.Value}");

        BulletsCount.Subscribe(value => BulletsCount.Value = Mathf.Clamp(value, 0, _maxBulletsCount));
    }

    public void SetMaxBulletsCount(int maxBulletsCount)
    {
        _maxBulletsCount = Mathf.Clamp(maxBulletsCount, 1, 10);
        _bulletHeight = BulletHeights[_maxBulletsCount];
        Reload();
    }

    public void Reload() => BulletsCount.Value = _maxBulletsCount;
    public void ReduceBullet() => BulletsCount.Value--;

    private void UpdateUI()
    {
        ClearBullets();
        for (int i = 0; i < BulletsCount.Value; i++)
        {
            var bullet = Instantiate(_bulletPrefab, _bulletsHolder);
            bullet.SetSize(_bulletHeight);
            _bulletsList.Add(bullet);
        }
    }

    private void ClearBullets()
    {
        foreach (var bullet in _bulletsList)
            Destroy(bullet.gameObject);
        _bulletsList.Clear();
    }
}
