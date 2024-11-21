using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet; 
    public int damage; 

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
}
