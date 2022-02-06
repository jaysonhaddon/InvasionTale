using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    idle,
    run,
    dash,
    meleeAttack,
    rangedAttack,
    interact
}
public class PlayerMaster : MonoBehaviour
{
    [Header("Player State Variables")]
    [SerializeField] private PlayerState currentState;

    [Header("Player Movement Variables")]
    [SerializeField] private Vector2 facingDirection;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] public bool canDash = true;

    [Header("Player Attack Variables")]
    [SerializeField] public bool canAttack = true;
    [SerializeField] private MeleeWeapon currentWeapon;

    [Header("Player Interaction Variables")]
    public bool canInteract = false;
    public Interactable currentInteractable;

    // Cached References
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private Rigidbody2D playerRb;
    private Animator playerAnim;

    // Get Set
    public PlayerState CurrentState { get { return currentState; } set { currentState = value; } }
    public Vector2 FacingDirection { get { return facingDirection; } }
    public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
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
    }

    private void PlayerMovementInput()
    {
        moveDirection.Normalize();

        if (currentState != PlayerState.meleeAttack && currentState != PlayerState.dash && currentState != PlayerState.interact)
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

    public void PlayerAttack()
    {
        if (canAttack)
        {
            if (currentWeapon != null && currentWeapon.enabled == true)
            {
                Debug.Log("Player is attacking with melee weapon!");
                currentState = PlayerState.meleeAttack;
                currentWeapon.PerformAttack();
                playerAnimation.PlayerMeleeAttackAnimation();
            }
        }
    }

    public void PlayerDash()
    {
        if (canDash)
        {
            Debug.Log("Player is dashing!");
            currentState = PlayerState.dash;
            playerMovement.StartDashing();
        }
    }

    public void SetThrustSpeed(float thrust)
    {
        playerMovement.ThrustSpeed = thrust;
    }

    public void PlayerInteractCheck()
    {
        if (canInteract)
        {
            Debug.Log("Player is interacting!");
            currentState = PlayerState.interact;
            currentInteractable.PerformInteraction();
        }
    }
}
