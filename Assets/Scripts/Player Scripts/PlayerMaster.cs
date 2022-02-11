using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    normal,
    hurt,
    action,
    pause,
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
    public bool holdingObject = false;
    public Interactable currentInteractable;
    public GameObject playerItemHolder;

    // Cached References
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    // Get Set
    public PlayerState CurrentState { get { return currentState; } set { currentState = value; } }
    public Vector2 FacingDirection { get { return facingDirection; } }
    public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }

    private void Awake()
    {
        currentState = PlayerState.normal;
        facingDirection = new Vector2(0, -1);
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerDirectionVectors();
    }

    private void SetPlayerDirectionVectors()
    {
        moveDirection.Normalize();

        if (moveDirection != Vector2.zero)
        {
            facingDirection = moveDirection;
        }

        /*if (currentState == PlayerState.normal)
        {
            if (moveDirection != Vector2.zero)
            {
                facingDirection = moveDirection;
            }
        }*/
    }

    public void PlayerAttack()
    {
        if (canAttack)
        {
            if (currentWeapon != null && currentWeapon.enabled == true)
            {
                Debug.Log("Player is attacking with melee weapon!");
                currentState = PlayerState.action;
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
            currentState = PlayerState.action;
            playerMovement.DashingMovement();
        }
    }

    public void SetActionSpeed(float speed)
    {
        playerMovement.ActionSpeed = speed;
    }

    public void PlayerInteractCheck()
    {
        if (canInteract)
        {
            Debug.Log("Player is interacting!");
            currentState = PlayerState.action;
            currentInteractable.PerformInteraction();
        }
    }

    public void PlayerHeldObjectActions()
    {
        if (!holdingObject)
        {
            playerAnimation.PlayerGrabObjectAnimation();
            playerMovement.HeldObjectMovement();
            holdingObject = true;
            canAttack = false;
            canDash = false;
        }
        else
        {
            playerAnimation.PlayerThrowObjectAnimation();
            playerMovement.HeldObjectMovement();
            holdingObject = false;
            canAttack = true;
            canDash = true;
        }
    }
}
