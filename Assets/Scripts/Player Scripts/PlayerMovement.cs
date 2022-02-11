using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float startMoveSpeed;
    [SerializeField] private float startActionSpeed;
    [SerializeField] private float actionSpeed;

    [Header("Player Dash Variables")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashingTime;
    [SerializeField] private float timeBtwDashes;

    [Header("Player Held Object Variables")]
    [SerializeField] private float heldObjectTime;

    // Get Set
    public float ActionSpeed { set { actionSpeed = value; } }

    // Cached References
    private PlayerMaster playerMaster;
    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerMaster = GetComponent<PlayerMaster>();
        playerRb = GetComponent<Rigidbody2D>();
        moveSpeed = startMoveSpeed;
        actionSpeed = startActionSpeed;
    }

    private void FixedUpdate()
    {
        if (playerMaster.CurrentState == PlayerState.normal)
        {
            NormalMovement();
        }
        else if (playerMaster.CurrentState == PlayerState.action)
        {
            ActionMovement();
        }
        else
        {
            StopMovement();
        }
    }

    public void StopMovement()
    {
        playerRb.velocity = Vector2.zero;
    }

    private void NormalMovement()
    {
        if (playerMaster.MoveDirection != Vector2.zero)
        {
            playerRb.velocity = playerMaster.MoveDirection * moveSpeed;
        }
        else
        {
            playerRb.velocity = Vector2.zero;
        }
        
    }

    private void ActionMovement()
    {
        playerRb.velocity = playerMaster.FacingDirection * actionSpeed;
    }

    IEnumerator DashCo()
    {
        actionSpeed = dashSpeed;
        playerMaster.canDash = false;
        yield return new WaitForSeconds(dashingTime);
        playerMaster.CurrentState = PlayerState.normal;
        actionSpeed = startActionSpeed;
        yield return new WaitForSeconds(timeBtwDashes);
        playerMaster.canDash = true;
    }

    IEnumerator ObjectHoldCo()
    {
        playerMaster.CurrentState = PlayerState.action;
        yield return new WaitForSeconds(heldObjectTime);
        moveSpeed = 1.5f;
        playerMaster.CurrentState = PlayerState.normal;
    }

    IEnumerator ObjectThrowCo()
    {
        playerMaster.CurrentState = PlayerState.action;
        yield return new WaitForSeconds(heldObjectTime);
        moveSpeed = startMoveSpeed;
        playerMaster.CurrentState = PlayerState.normal;
    }

    public void DashingMovement()
    {
        StartCoroutine(DashCo());
    }

    public void HeldObjectMovement()
    {
        if (!playerMaster.holdingObject)
        {
            StartCoroutine(ObjectHoldCo());
        }
        else
        {
            StartCoroutine(ObjectThrowCo());
        }
    }
}
