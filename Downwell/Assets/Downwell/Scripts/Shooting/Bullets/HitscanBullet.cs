using UnityEngine;

public class HitscanBullet : Bullet
{
    [SerializeField] private float _maxDistance = 50f;
    [SerializeField] private LayerMask _hitLayer;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _lineDuration = 0.1f;

    public override void Init(float speed, Vector3 direction)
    {
        base.Init(speed, direction);

        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + direction.normalized * _maxDistance;

        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, _maxDistance, _hitLayer);

        if (hit.collider != null)
        {
            endPoint = hit.point;

            if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(Constants.BulletDamage);
                Debug.Log($"Hitscan");
            }
            else
                Debug.Log($"No");
        }

        DrawLine(startPoint, endPoint);
        Destroy(gameObject, _lineDuration);
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
        _lineRenderer.enabled = true;
        Invoke(nameof(HideLine), _lineDuration);
    }

    private void HideLine()
    {
        _lineRenderer.enabled = false;
    }
}
