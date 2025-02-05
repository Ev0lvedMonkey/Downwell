using R3;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Simple,
    Shotgun,
    Lazer,
    ExtraSimple,
    ThirdBullet
}

public class BaseShooter : MonoBehaviour, IShooter
{
    [Header("Components")]
    [SerializeField] private Transform _shotPostion;

    private const string MainPath = "Configs/";
    private IControllable _controllable;
    private StreamBus _streamBus;
    private ClipView _clipView;
    private WeaponConfig _weaponConfig;
    protected int _bulletsCount;
    private float _nextShotTime;

    private Dictionary<WeaponType, WeaponConfig> _weaponDicitionry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Platform platform))
            Reload();
    }

    public void Init(IControllable controllable)
    {
        _weaponDicitionry = new()
            {
                {WeaponType.Simple, Resources.Load<WeaponConfig>($"{MainPath}SimpleWeaponConfig")},
                {WeaponType.Shotgun, Resources.Load<WeaponConfig>($"{MainPath}ShotgunWeaponConfig")},
                {WeaponType.Lazer, Resources.Load<WeaponConfig>($"{MainPath}LazerWeaponConfig")},
                {WeaponType.ThirdBullet, Resources.Load<WeaponConfig>($"{MainPath}{WeaponType.ThirdBullet}WeaponConfig")}
            };
        _streamBus = ServiceLocator.Current.Get<StreamBus>();
        _clipView = ServiceLocator.Current.Get<ClipView>();
        _controllable = controllable;
        SetNewWeapon(WeaponType.Simple);
        Reload();
    }

    public void SetNewWeapon(WeaponType weaponType)
    {
        if (!_weaponDicitionry.TryGetValue(weaponType, out _weaponConfig) || _weaponConfig == null)
        {
            Debug.LogError($"[BaseShooter] WeaponConfig not found or failed to load for {weaponType}");
            return;
        }

        _weaponConfig = _weaponDicitionry[weaponType];
        _clipView.SetMaxBulletsCount(_weaponConfig.ClipVolume);
        Debug.Log($"[BaseShooter] Switched to weapon: {weaponType}");
    }


    public void Reload()
    {
        _bulletsCount = _weaponConfig.ClipVolume;
    }

    public void Shot()
    {
        if (_controllable.IsOnTheGround())
            return;
        if (_bulletsCount <= 0 || Time.time < _nextShotTime)
            return;

        UniqShot();
        _controllable.Jump(_weaponConfig.RepulsiveForce);
        _bulletsCount--;
        _streamBus.OnShotEvent.OnNext(Unit.Default);

        _nextShotTime = Time.time + _weaponConfig.FireRate;
    }


    private void UniqShot()
    {
        for (int i = 0; i < _weaponConfig.BulletsCountByShot; i++)
        {
            float randomOffset = Random.Range(-_weaponConfig.BulletLeftOffest, _weaponConfig.BulletRightOffest);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, randomOffset);

            Bullet bullet = Instantiate(_weaponConfig.BulletPrefab, _shotPostion.position, bulletRotation);
            bullet.Init(_weaponConfig.BulletSpeed, _weaponConfig.BulletLifeTime, bulletRotation * Vector3.down);
        }
    }
}

