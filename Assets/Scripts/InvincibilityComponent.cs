using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7; 
    [SerializeField] private float blinkInterval = 0.1f; 
    [SerializeField] private Material blinkMaterial; 

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial; 

    public bool isInvincible = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    /// <summary>
    /// </summary>
    public void TriggerInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    /// <summary>
    /// </summary>
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; 

        for (int i = 0; i < blinkingCount; i++)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.material = blinkMaterial;
            }
            yield return new WaitForSeconds(blinkInterval / 2);

            if (spriteRenderer != null)
            {
                spriteRenderer.material = originalMaterial;
            }
            yield return new WaitForSeconds(blinkInterval / 2);
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.material = originalMaterial;
        }

        isInvincible = false;
    }
}

