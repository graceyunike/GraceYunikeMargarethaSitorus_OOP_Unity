using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private PlayerMovement playerMovement;
    private Animator animator;

    private HealthComponent healthComponent;
    private AttackComponent attackComponent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();

        healthComponent = GetComponent<HealthComponent>();
        attackComponent = GetComponent<AttackComponent>();
    }

    private void FixedUpdate()
    {
        playerMovement.moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerMovement.Move();
    }

    private void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.IsMoving());

        if (Input.GetKeyDown(KeyCode.Space) && attackComponent != null) 
        {
            attackComponent.Attack();
        }
    }
}
