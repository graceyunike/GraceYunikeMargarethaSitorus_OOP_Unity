using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField]
    private HealthComponent health;  

    private InvincibilityComponent invincibilityComponent;

    private void Start()
    {
        invincibilityComponent = GetComponent<InvincibilityComponent>();  
    }

    public void Damage(Bullet bullet)
    {
        if (invincibilityComponent != null && invincibilityComponent.isInvincible) return;

        if (health != null)
        {
            health.Subtract(bullet.damage);  
        }
    }

    public void Damage(int damage)
    {
        if (invincibilityComponent != null && invincibilityComponent.isInvincible) return;

        if (health != null)
        {
            health.Subtract(damage);  
        }
    }
}
