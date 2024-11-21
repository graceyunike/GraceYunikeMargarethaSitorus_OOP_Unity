using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMovement : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 direction;

    private void Awake()
    {
        PickRandomPositions();
    }

    private void Update()
    {
        // Move horizontally
        transform.Translate(moveSpeed * Time.deltaTime * direction);

        // Check for screen bounds and respawn if necessary
        Vector3 enemyViewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if ((enemyViewportPos.x < -0.05f && direction == Vector2.right) ||
            (enemyViewportPos.x > 1.05f && direction == Vector2.left))
        {
            PickRandomPositions();
        }
    }

    private void PickRandomPositions()
    {
        // Determine spawn side and direction
        direction = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        Vector2 randomPos = direction == Vector2.right
            ? new Vector2(-0.1f, Random.Range(0.1f, 0.9f))
            : new Vector2(1.1f, Random.Range(0.1f, 0.9f));

        transform.position = Camera.main.ViewportToWorldPoint(randomPos) + new Vector3(0, 0, 10);
    }
}
