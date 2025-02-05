using UnityEngine;
using System.Collections;

public class IndentityEnemiesAround : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private LayerMask _enemyLayer;

    private void Start()
    {
        StartCoroutine(CheckEnemiesCoroutine());
    }

    private IEnumerator CheckEnemiesCoroutine()
    {
        while (true)
        {
            CheckEnemies();
            yield return new WaitForSeconds(1f);
        }
    }

    private void CheckEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);

        foreach (Collider2D enemyCollider in enemies)
        {
            if (enemyCollider.TryGetComponent<FlyingEnemyMovement>(out var enemy))
                enemy.SetTarget(transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
