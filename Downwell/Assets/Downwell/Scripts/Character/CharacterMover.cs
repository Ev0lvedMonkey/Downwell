using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterMover : MonoBehaviour, IControllable
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Move Properties")]
    [SerializeField, Range(100f, 1000f)] private float _moveSpeed;

    [Header("Jump Properties")]
    [SerializeField, Range(1f, 5f)] private float _jumpForce;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField, Range(0.1f, 2f)] private float _checkSphereRaduis;
    [SerializeField] private Transform _groundCheckDot;

    private Vector2 _movementDirection;
    private bool _isGrounded;
    private bool _facingRight = true;
    private float _fallTime = 0f; 

    private const float MaxFallSpeed = -1f; 
    private const float FallLimitDelay = 0.5f; 

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = IsOnTheGround();

        if (_isGrounded)
            _fallTime = 0f; 
        else
            _fallTime += Time.fixedDeltaTime; 

        MoveInternal();
        LimitFallSpeed();
    }

    public void Move(Vector2 direction)
    {
        _movementDirection = direction;

        if (direction.x > 0 && !_facingRight)
            Flip();
        else if (direction.x < 0 && _facingRight)
            Flip();
    }

    public void Jump()
    {
        if (_isGrounded)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void MoveInternal()
    {
        _rigidbody.velocity = new Vector2(_movementDirection.x * _moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    private void LimitFallSpeed()
    {
        if (_fallTime >= FallLimitDelay && _rigidbody.velocity.y < MaxFallSpeed)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, MaxFallSpeed);
    }

    private bool IsOnTheGround()
    {
        return Physics2D.OverlapCircle(_groundCheckDot.position, _checkSphereRaduis, _groundMask) != null;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
