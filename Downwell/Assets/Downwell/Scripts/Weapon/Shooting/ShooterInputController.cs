using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseShooter))]
[RequireComponent(typeof(IControllable))]
public class ShooterInputController : MonoBehaviour
{
    private CharacterMover _controllable;
    private BaseShooter _shooter;
    private GameInput _gameInput;
    private bool _isShooting;

    public void Init()
    {
        _controllable = GetComponent<CharacterMover>();
        _shooter = GetComponent<BaseShooter>();
        _gameInput = new();
        _gameInput.Enable();
        _shooter.Init(_controllable);
    }

    private void Start()
    {
        _gameInput.Gameplay.Shot.performed += StartShooting;
        _gameInput.Gameplay.Shot.canceled += StopShooting;
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Shot.performed -= StartShooting;
        _gameInput.Gameplay.Shot.canceled -= StopShooting;
    }

    private void StartShooting(InputAction.CallbackContext context)
    {
        if (_controllable.IsOnTheGround())
            return;
        _isShooting = true;
    }

    private void StopShooting(InputAction.CallbackContext context)
    {
        _isShooting = false;
    }

    private void Update()
    {
        if (_isShooting)
            _shooter.Shot();
    }
}
