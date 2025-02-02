using UnityEngine;
using static TreeEditor.TreeEditorHelper;

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
    private bool _facingRight = true;

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveInternal();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlatformTrigger platform))
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log($"velocity zero");
        }
    }

    public void Move(Vector2 direction)
    {
        _movementDirection = direction;

        if (_movementDirection.x > 0 && !_facingRight)
            Flip();
        else if (_movementDirection.x < 0 && _facingRight)
            Flip();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    public bool IsOnTheGround()
    {
        return Physics2D.OverlapCircle(_groundCheckDot.position, _checkSphereRaduis, _groundMask) != null;
    }

    private void MoveInternal()
    {
        if (IsTouchingWall())
            return;
        _rigidbody.velocity =
            new(_movementDirection.x * _moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);

    }

    private bool IsTouchingWall()
    {
        float direction = _facingRight ? 1f : -1f;
        Vector2 origin = transform.position;
        float distance = 0.1f;

        return Physics2D.Raycast(origin, Vector2.right * direction, distance, _groundMask);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
