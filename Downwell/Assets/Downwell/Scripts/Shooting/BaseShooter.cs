using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType
{
    MachineGun,
    Lazer,
    Noppy,
    Shotgun,
    Puncher,
    Tripl
}

public class BaseShooter : MonoBehaviour, IShooter
{
    [Header("Deafault Weapon Type")]
    [SerializeField] private WeaponType _defaultWeaponType;

    [Header("Components")]
    [SerializeField] private Transform _shotPostion;

    private const string MainPath = "Configs/";
    private CharacterMover _controllable;
    private StreamBus _streamBus;
    private ClipView _clipView;
    private WeaponConfig _weaponConfig;
    private int _bulletsCount;
    private float _nextShotTime;

    private int _currentClipVolume;
    private float _currentBulletLifeTime;

    private Dictionary<WeaponType, WeaponConfig> _weaponDicitionry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Platform platform))
            Reload();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            IncreaseAmmo(1);
        if (Input.GetKeyDown(KeyCode.I))
            ResetUpgrades();
    }

    public void Init(CharacterMover controllable)
    {
        _weaponDicitionry = new()
        {
            {WeaponType.MachineGun, Resources.Load<WeaponConfig>($"{MainPath}MachineGunWeaponConfig")},
            {WeaponType.Lazer, Resources.Load<WeaponConfig>($"{MainPath}LazerWeaponConfig")},
            {WeaponType.Noppy, Resources.Load<WeaponConfig>($"{MainPath}NoppyWeaponConfig")},
            {WeaponType.Shotgun, Resources.Load<WeaponConfig>($"{MainPath}ShotgunWeaponConfig")},
            {WeaponType.Puncher, Resources.Load<WeaponConfig>($"{MainPath}PuncherWeaponConfig")},
            {WeaponType.Tripl, Resources.Load<WeaponConfig>($"{MainPath}TriplWeaponConfig")},
        };
        _streamBus = ServiceLocator.Current.Get<StreamBus>();
        _clipView = ServiceLocator.Current.Get<ClipView>();
        _controllable = controllable;
        SetNewWeapon(_defaultWeaponType);
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

        _currentClipVolume = _weaponConfig.ClipVolume;
        _currentBulletLifeTime = _weaponConfig.BulletLifeTime;

        _clipView.SetMaxBulletsCount(_currentClipVolume);
        Debug.Log($"[BaseShooter] Switched to weapon: {weaponType}");
    }

    public void Reload()
    {
        _bulletsCount = _currentClipVolume;
        _clipView.SetMaxBulletsCount(_currentClipVolume);
    }

    public void Shot()
    {
        if (_controllable.IsOnTheGround() || _bulletsCount <= 0 || Time.time < _nextShotTime)
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
            float offset;
            if (_weaponConfig.Type == WeaponType.Noppy)
                offset = _controllable.FacingRight ? _weaponConfig.BulletRightOffest : -_weaponConfig.BulletLeftOffest;
            else
                offset = Random.Range(-_weaponConfig.BulletLeftOffest, _weaponConfig.BulletRightOffest);

            Quaternion bulletRotation = Quaternion.Euler(0, 0, offset);
            Bullet bullet = Instantiate(_weaponConfig.BulletPrefab, _shotPostion.position, bulletRotation);
            bullet.Init(_weaponConfig.BulletSpeed, _weaponConfig.BulletDamage, _currentBulletLifeTime, bulletRotation * Vector3.down);
        }
    }


    public void IncreaseAmmo(int amount)
    {
        _currentClipVolume += amount;

        Reload();
        Debug.Log($"[BaseShooter] Ammo increased by {amount}. New clip volume: {_currentClipVolume}");
    }

    public void IncreaseBulletLifeTime(float amount)
    {
        _currentBulletLifeTime += amount;
        Debug.Log($"[BaseShooter] Bullet lifetime increased by {amount}. New bullet lifetime: {_currentBulletLifeTime}");
    }

    public void ResetUpgrades()
    {
        _currentClipVolume = _weaponConfig.ClipVolume;
        _currentBulletLifeTime = _weaponConfig.BulletLifeTime;
        Reload();
        Debug.Log("[BaseShooter] Upgrades reset to default values.");
    }
}

