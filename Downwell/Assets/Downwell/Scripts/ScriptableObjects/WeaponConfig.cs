using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "ScriptableObjects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [Header("WeapoonType")]
    [SerializeField] private WeaponType _weaponType;

    [Header("Properties")]
    [SerializeField, Range(1, 10)] private int _clipVolume;
    [SerializeField, Range(1, 10)] private float _repulsiveForce;
    [SerializeField, Range(0.1f, 2f)] private float _fireRate;
    [SerializeField, Range(1, 10)] private int _bulletsCountByShot;
    [SerializeField, Range(0, 45)] private float _bulletLeftOffest;
    [SerializeField, Range(0, 45)] private float _bulletRightOffest;


    [Header("Bullet Properties")]
    [SerializeField] private Bullet _bulletPrefab;  
    [SerializeField, Range(1, 45)] private float _bulletSpeed;
    [SerializeField, Range(0.1f, 5f)] private float _bulletLifeTime;
    [SerializeField, Range(0.5f, 5f)] private float _bulletDamage;

    public WeaponType Type => _weaponType;
    public int ClipVolume => _clipVolume;
    public float RepulsiveForce => _repulsiveForce;
    public int BulletsCountByShot => _bulletsCountByShot;
    public float BulletLeftOffest => _bulletLeftOffest;
    public float BulletRightOffest => _bulletRightOffest;
    public float BulletSpeed => _bulletSpeed;
    public float BulletLifeTime => _bulletLifeTime;
    public float FireRate => _fireRate;
    public float BulletDamage => _bulletDamage;
    public Bullet BulletPrefab => _bulletPrefab;
}