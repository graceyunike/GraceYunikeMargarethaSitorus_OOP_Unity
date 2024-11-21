using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForwardMovement : Enemy
{
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        PickRandomPositions();
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.down);

        if (Camera.main.WorldToViewportPoint(transform.position).y < -0.05f)
        {
            PickRandomPositions();
        }
    }

    private void PickRandomPositions()
    {
        Vector2 randomPos = new Vector2(Random.Range(0.1f, 0.9f), 1.1f);
        transform.position = Camera.main.ViewportToWorldPoint(randomPos) + new Vector3(0, 0, 10);
    }
}

