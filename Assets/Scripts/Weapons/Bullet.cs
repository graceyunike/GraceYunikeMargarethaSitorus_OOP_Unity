using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;

    private Rigidbody2D rb;
    public IObjectPool<Bullet> objectPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Obstacle"))
        {
            HitboxComponent hitbox = collision.GetComponent<HitboxComponent>();
            if (hitbox != null && collision.CompareTag("Enemy"))
            {
                hitbox.Damage(damage);
            }

            ReturnToPool();
        }
    }

    private void OnBecameInvisible()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
