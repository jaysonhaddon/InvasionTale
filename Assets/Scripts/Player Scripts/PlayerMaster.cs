using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    idle,
    run,
    meleeAttack,
    rangedAttack,
    pause
}
public class PlayerMaster : MonoBehaviour
{
    [Header("Player State Variables")]
    [SerializeField] private PlayerState currentState;

    [Header("Player Movement Variables")]
    [SerializeField] private Vector2 facingDirection;
    [SerializeField] private Vector2 moveDirection;

    [Header("Player Attack Variables")]
    [SerializeField] public bool canAttack = true;

    [SerializeField] private MeleeWeapon currentWeapon;

    // Cached References
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private Rigidbody2D playerRb;
    private Animator playerAnim;

    // Get Set
    public PlayerState CurrentState { get { return currentState; } set { currentState = value; } }
    public Vector2 FacingDirection { get { return facingDirection; } }
    public Vector2 MoveDirection { get { return moveDirection; } }
    public Rigidbody2D PlayerRb { get { return playerRb; } }
    public Animator PlayerAnim {  get { return playerAnim; } }

    private void Awake()
    {
        currentState = PlayerState.idle;
        facingDirection = new Vector2(0, -1);
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput();
        PlayerAttackInput();
    }

    private void PlayerMovementInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();

        if (currentState != PlayerState.meleeAttack)
        {
            if (moveDirection != Vector2.zero)
            {
                currentState = PlayerState.run;
                facingDirection = moveDirection;
            }
            else
            {
                currentState = PlayerState.idle;
            }
        }
    }

    private void PlayerAttackInput()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            if (currentWeapon != null && currentWeapon.enabled == true)
            {
                currentState = PlayerState.meleeAttack;
                currentWeapon.PerformAttack();
                playerAnimation.PlayerMeleeAttackAnimation();
            }
        }
    }

    public void SetThrustSpeed(float thrust)
    {
        playerMovement.ThrustSpeed = thrust;
    }
}
