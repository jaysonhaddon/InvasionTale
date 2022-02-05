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

    private void Awake()
    {
        playerMaster = GetComponent<PlayerMaster>();
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

    private void PlayerStop()
    {
        playerMaster.PlayerRb.velocity = Vector2.zero;
    }

    private void PlayerRun()
    {
        playerMaster.PlayerRb.velocity = playerMaster.MoveDirection * moveSpeed;
    }

    private void PlayerDash()
    {
        playerMaster.PlayerRb.velocity = playerMaster.FacingDirection * dashSpeed;
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
        playerMaster.PlayerRb.velocity = playerMaster.FacingDirection * thrustSpeed; 
    }

    public void StartDashing()
    {
        StartCoroutine(DashCo());
    }
}
