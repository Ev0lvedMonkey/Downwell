using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseShooter))]
[RequireComponent(typeof(IControllable))]
public class ShooterInputController : MonoBehaviour
{
    private IControllable _controllable;
    private BaseShooter _shooter;
    private GameInput _gameInput;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _controllable = GetComponent<IControllable>();
        _shooter = GetComponent<BaseShooter>();
        _gameInput = new();
        _gameInput.Enable();
        _shooter.Init(_controllable);
    }

    private void OnEnable()
    {
        _gameInput.Gameplay.Shot.performed += Shot;
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Shot.performed -= Shot;
    }

    private void Shot(InputAction.CallbackContext context)
    {
        if (_controllable.IsOnTheGround())
            return;
        _shooter.Shot();
    }
}

