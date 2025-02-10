using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private TextMeshProUGUI _weaponText;

    private bool _weaponChanged = false;

    private static readonly Dictionary<WeaponType, string> _weaponInitials = new()
    {
        { WeaponType.Noppy, "N" },
        { WeaponType.Lazer, "L" },
        { WeaponType.Tripl, "T" },
        { WeaponType.Puncher, "P" },
        { WeaponType.MachineGun, "M" }
    };

    private void Start()
    {
        UpdateWeaponText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseShooter shooter))
        {
            shooter.SetNewWeapon(_weaponType);
            Destroy(gameObject);
        }
        else if (collision.TryGetComponent(out Bullet bullet))
        {
            if (_weaponChanged == true)
                return;
            ChangeWeaponTypeRandomly();
            UpdateWeaponText();
            _weaponChanged = true;
        }
    }

    private void ChangeWeaponTypeRandomly()
    {
        WeaponType newType;
        do
        {
            newType = (WeaponType)Random.Range(0, Enum.GetValues(typeof(WeaponType)).Length);
        }
        while (newType == _weaponType);

        _weaponType = newType;
    }

    private void UpdateWeaponText()
    {
        if (_weaponText != null && _weaponInitials.TryGetValue(_weaponType, out string initial))
        {
            _weaponText.text = initial;
        }
    }

    public void SetWeaponType(WeaponType newType)
    {
        _weaponType = newType;
        UpdateWeaponText();
    }
}
