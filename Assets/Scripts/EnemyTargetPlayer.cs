using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPlayer : Enemy
{
    [SerializeField] private float moveSpeed = 2f;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

