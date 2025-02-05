using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _stopDistance = 0.1f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;

    private int _currentWaypointIndex = 0;
    private Vector2 _lastDirection;

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_wayPoints.Length == 0)
        {
            Debug.LogError("[PatrolMovement] Нет точек маршрута!");
        }
    }

    private void OnEnable()
    {
        if (_wayPoints.Length < 0)
            return;
        SetNextWaypoint();
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }
    private void FixedUpdate()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        if (_wayPoints.Length == 0) return;

        Vector2 targetPos = _wayPoints[_currentWaypointIndex].position;
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        if (Vector2.Distance(transform.position, targetPos) < _stopDistance)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
            SetNextWaypoint();
            return;
        }

        _rb.velocity = direction * _speed;
    }

    private void SetNextWaypoint()
    {
        Vector2 targetPos = _wayPoints[_currentWaypointIndex].position;
        Vector2 newDirection = (targetPos - (Vector2)transform.position).normalized;

        if (newDirection != _lastDirection)
        {
            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            _lastDirection = newDirection;
        }
    }

}
