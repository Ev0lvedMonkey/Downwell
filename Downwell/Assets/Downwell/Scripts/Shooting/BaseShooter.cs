using UnityEngine;

public abstract class BaseShooter : MonoBehaviour, IShooter
{
    [Header("Shot Properties")]
    [SerializeField, Range(1, 10)] protected int _clipVolume;
    [SerializeField, Range(1, 100)] protected float _repulsiveForce;
    [SerializeField, Range(1, 10)] protected int _bulletsCountByShot;
    [SerializeField, Range(0, 45)] protected float _bulletLeftOffest;
    [SerializeField, Range(0, 45)] protected float _bulletRightOffest;

    private IControllable _controllable;
    protected int _bulletsCount;

    public virtual void Init(IControllable controllable)
    {
        _controllable = controllable;
        Reload();
    }

    public virtual void Reload()
    {
        _bulletsCount = _clipVolume;
    }

    public virtual void Shot()
    {
        if (_bulletsCount > 0)
            _controllable.Jump();
        _bulletsCount--;
    }
}

