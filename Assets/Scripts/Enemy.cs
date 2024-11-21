using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Horizontal, Forward, Targeting, Boss }
    public EnemyType enemyType;

    [SerializeField] protected int level;
    public UnityEvent enemyKilledEvent;

    private HealthComponent healthComponent;
    private HitboxComponent hitboxComponent;

    private void Start()
    {
        enemyKilledEvent ??= new UnityEvent();

        switch (enemyType)
        {
            case EnemyType.Horizontal:
                level = 1;
                break;
            case EnemyType.Forward:
                level = 2;
                break;
            case EnemyType.Targeting:
                level = 3;
                break;
            case EnemyType.Boss:
                level = 5;
                break;
            default:
                level = 1;
                break;
        }

        healthComponent = GetComponent<HealthComponent>();
        hitboxComponent = GetComponent<HitboxComponent>();

        Debug.Log("Enemy Level: " + level);
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }

    private void OnDestroy()
    {
        enemyKilledEvent.Invoke();
    }
}
