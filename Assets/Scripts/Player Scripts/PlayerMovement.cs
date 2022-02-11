using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float dashSpeed;

    // Get Set
    public float ThrustSpeed { set { thrustSpeed = value; } }

    // Cached References
    private PlayerMaster playerMaster;
    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerMaster = GetComponent<PlayerMaster>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerMaster.CurrentState == PlayerState.run)
        {
            PlayerRun();
        }
        else if (playerMaster.CurrentState == PlayerState.dash)
        {
            PlayerDash();
        }
        else if (playerMaster.CurrentState == PlayerState.meleeAttack)
        {
            PlayerThrust();
        }
        else
        {
            PlayerStop();
        }
    }

    public void PlayerStop()
    {
        playerRb.velocity = Vector2.zero;
    }

    private void PlayerRun()
    {
        playerRb.velocity = playerMaster.MoveDirection * moveSpeed;
    }

    private void PlayerDash()
    {
        playerRb.velocity = playerMaster.FacingDirection * dashSpeed;
    }

    IEnumerator DashCo()
    {
        playerMaster.canDash = false;
        yield return new WaitForSeconds(.15f);
        playerMaster.CurrentState = PlayerState.idle;
        yield return new WaitForSeconds(2f);
        playerMaster.canDash = true;
    }

    private void PlayerThrust()
    {
        playerRb.velocity = playerMaster.FacingDirection * thrustSpeed; 
    }

    public void StartDashing()
    {
        StartCoroutine(DashCo());
    }
}
