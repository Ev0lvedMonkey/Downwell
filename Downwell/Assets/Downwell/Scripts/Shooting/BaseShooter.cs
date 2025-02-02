using R3;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public abstract class BaseShooter : MonoBehaviour, IShooter
{
    [Header("Shot Properties")]
    [SerializeField, Range(1, 10)] protected int _clipVolume;
    [SerializeField, Range(1, 10)] protected float _repulsiveForce;
    [SerializeField, Range(1, 10)] protected int _bulletsCountByShot;
    [SerializeField, Range(0, 45)] protected float _bulletLeftOffest;
    [SerializeField, Range(0, 45)] protected float _bulletRightOffest;

    private IControllable _controllable;
    private StreamBus _streamBus;
    private ClipView _clipView;
    protected int _bulletsCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlatformTrigger platform))
        {
            Reload();
        }
    }

    public virtual void Init(IControllable controllable)
    {
        _streamBus = ServiceLocator.Current.Get<StreamBus>();
        _clipView = ServiceLocator.Current.Get<ClipView>();
        _clipView.SetMaxBulletsCount(_clipVolume);
        _controllable = controllable;
        Reload();
    }

    public virtual void Reload()
    {
        _bulletsCount = _clipVolume;
    }

    public virtual void Shot()
    {
        if (_bulletsCount < 0)
            return;
        _controllable.Jump(_repulsiveForce);
        _bulletsCount--;
        _streamBus.OnShotEvent.OnNext(Unit.Default);
    }
}

