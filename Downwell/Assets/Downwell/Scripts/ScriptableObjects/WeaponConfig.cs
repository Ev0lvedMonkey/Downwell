using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "ScriptableObjects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [Header("Properties")]
    [SerializeField, Range(1, 10)] private int _clipVolume;
    [SerializeField, Range(1, 10)] private float _repulsiveForce;
    [SerializeField, Range(1, 10)] private int _bulletsCountByShot;
    [SerializeField, Range(0, 45)] private float _bulletLeftOffest;
    [SerializeField, Range(0, 45)] private float _bulletRightOffest;
    [SerializeField, Range(0, 45)] private float _bulletSpeed;


    [Header("Prefab")]
    [SerializeField] private Bullet _bulletPrefab;  

    public int ClipVolume => _clipVolume;
    public float RepulsiveForce => _repulsiveForce;
    public int BulletsCountByShot => _bulletsCountByShot;
    public float BulletLeftOffest => _bulletLeftOffest;
    public float BulletRightOffest => _bulletRightOffest;
    public float BulletSpeed => _bulletSpeed;

    public Bullet BulletPrefab => _bulletPrefab;
}