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
    private IObjectPool<Bullet> pool; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = Vector2.up * bulletSpeed;
    }

    public void SetPool(IObjectPool<Bullet> objectPool)
    {
        pool = objectPool;
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
    if (pool != null)
    {
        pool.Release(this); 
    }
    else
    {
        Destroy(gameObject); 
    }
}

}
