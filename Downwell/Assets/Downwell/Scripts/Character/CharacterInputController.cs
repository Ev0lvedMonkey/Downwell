using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(IControllable))]
public class CharacterInputController : MonoBehaviour
{
    private GameInput _gameInput;
    private IControllable _controllable;

    private void Update()
    {
        ReadMove();
    }

    private void Start()
    {
        _gameInput.Gameplay.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Jump.performed -= OnJumpPerformed;
    }

    public void Init()
    {
        _controllable = GetComponent<IControllable>();
        _gameInput = new();
        _gameInput.Enable();
    }

    private void ReadMove()
    {
        Vector2 inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        _controllable.Move(inputDirection);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (_controllable.IsOnTheGround())
            _controllable.Jump();
    }

}
