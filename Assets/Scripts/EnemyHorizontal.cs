using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 5f;  
    private bool movingRight = true;  
    public float spawnSide = 0f; 
    {
        base.Start();  
        
        spawnSide = Random.Range(0f, 1f);  
        {
            transform.position = new Vector2(-10f, transform.position.y);  
            movingRight = true;
        }
        else
        {
            transform.position = new Vector2(10f, transform.position.y);  
            movingRight = false;
        }
    }

    private void Update()
    {
        MoveEnemy();
        CheckBounds();
    }

    private void MoveEnemy()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void CheckBounds()
    {
        if (transform.position.x > 10f && movingRight)  
        {
            movingRight = false;  
        }
        else if (transform.position.x < -10f && !movingRight)  
        {
            movingRight = true;  
        }
    }
}
