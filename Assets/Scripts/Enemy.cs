using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Horizontal, Forward, Targeting, Boss }
    public EnemyType enemyType;  
    public Transform playerTransform;
    public int level; 

    private void Start()
    {
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

        Debug.Log("Enemy Level: " + level);
    }

    private void Update()
{
    Vector2 direction = (playerTransform.position - transform.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
}
}