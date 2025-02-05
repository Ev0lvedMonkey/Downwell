using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingEnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _stopDistance = 0.5f;
    [SerializeField] private Transform _target;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;

    private Transform _currentWaypoint;
    private List<Transform> _availableWaypoints = new();
    private bool _hasTarget => _target != null;
    private bool _facingRight = true;

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _rb.gravityScale = 0;
        _availableWaypoints = new List<Transform>(_waypoints);
        SetRandomWaypoint();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (_hasTarget)
            MoveTowards(_target.position);
        else if (_currentWaypoint != null)
        {
            MoveTowards(_currentWaypoint.position);

            if (Vector2.Distance(transform.position, _currentWaypoint.position) < _stopDistance)
                SetRandomWaypoint();
        }
    }

    private void MoveTowards(Vector2 targetPos)
    {
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        _rb.velocity = direction * _speed;
        RotateToDirection(direction);
    }

    private void RotateToDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > 0.01f) 
        {
            if (direction.x > 0 && !_facingRight)
                Flip();
            else if (direction.x < 0 && _facingRight)
                Flip();
        }
    }


    private void SetRandomWaypoint()
    {
        if (_availableWaypoints.Count == 0) return;

        Transform newWaypoint;
        do
        {
            newWaypoint = _availableWaypoints[Random.Range(0, _availableWaypoints.Count)];
        } while (newWaypoint == _currentWaypoint && _availableWaypoints.Count > 1);

        _currentWaypoint = newWaypoint;
    }


    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    public void ClearTarget()
    {
        _target = null;
        SetRandomWaypoint();
    }
}
