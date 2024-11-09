using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private Camera mainCamera;
    private Vector2 screenBounds;

    public Vector2 maxSpeed = new Vector2(5f, 5f);
    public Vector2 timeToFullSpeed = new Vector2(1f, 1f); 
    public Vector2 timeToStop = new Vector2(0.5f, 0.5f); 
    public Vector2 stopClamp = new Vector2(2.5f, 2.5f);

    private Vector2 currentSpeed; 
    public Vector2 moveDirection; 
    private Vector2 moveVelocity; 
    private Vector2 moveFriction; 
    private Vector2 stopFriction; 
    private bool isMoving = false; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        moveVelocity = new Vector2(
            2 * maxSpeed.x / timeToFullSpeed.x,
            2 * maxSpeed.y / timeToFullSpeed.y
        );

        moveFriction = new Vector2(
            -2 * maxSpeed.x / (timeToFullSpeed.x * timeToFullSpeed.x),
            -2 * maxSpeed.y / (timeToFullSpeed.y * timeToFullSpeed.y)
        );

        stopFriction = new Vector2(
            -2 * maxSpeed.x / (timeToStop.x * timeToStop.x),
            -2 * maxSpeed.y / (timeToStop.y * timeToStop.y)
        );
    }

    void FixedUpdate()
    {
        Move();
        MoveBound(); // Call MoveBound to keep the player within screen bounds
    }

    public void Move()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W)) verticalInput = 1f; 
        if (Input.GetKey(KeyCode.S)) verticalInput = -1f; 
        if (Input.GetKey(KeyCode.D)) horizontalInput = 1f; 
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f; 

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        Vector2 appliedVelocity = new Vector2(
            moveVelocity.x * moveDirection.x,
            moveVelocity.y * moveDirection.y
        );

        rb.velocity = new Vector2(moveDirection.x * maxSpeed.x, moveDirection.y * maxSpeed.y);
    }

    private Vector2 GetFriction()
    {
        Vector2 appliedFriction = Vector2.zero;

        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            appliedFriction.x = isMoving ? moveFriction.x : stopFriction.x;
        }

        if (Mathf.Abs(rb.velocity.y) > 0)
        {
            appliedFriction.y = isMoving ? moveFriction.y : stopFriction.y;
        }

        return appliedFriction;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }

    private void MoveBound()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);
        transform.position = pos;
    }
}
