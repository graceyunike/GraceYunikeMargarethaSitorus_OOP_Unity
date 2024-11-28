using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet; 
    public int damage; 
    public GameObject bulletPrefab;
    public Transform firePoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag)) return;

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            if (bullet != null)
            {
                hitbox.Damage(bullet.damage);  
            }

            hitbox.Damage(damage);  
        }

        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
        if (invincibility != null)
        {
            invincibility.TriggerInvincibility();  
        }
    }

    public void Attack()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("BulletPrefab atau FirePoint tidak disetel pada AttackComponent.");
        }
    }
}
