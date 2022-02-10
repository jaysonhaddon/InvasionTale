using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Cached References
    private PlayerMaster playerMaster;

    private void Awake()
    {
        playerMaster = GetComponent<PlayerMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorVariables();
        if (playerMaster.CurrentState != PlayerState.meleeAttack && !playerMaster.holdingObject)
        {
            PlayerRunAnimation();
        }
    }

    private void AnimatorVariables()
    {
        playerMaster.PlayerAnim.SetFloat("moveX", playerMaster.MoveDirection.x);
        playerMaster.PlayerAnim.SetFloat("moveY", playerMaster.MoveDirection.y);
        playerMaster.PlayerAnim.SetFloat("idleX", playerMaster.FacingDirection.x);
        playerMaster.PlayerAnim.SetFloat("idleY", playerMaster.FacingDirection.y);
    }

    private void PlayerRunAnimation()
    {
        if (playerMaster.MoveDirection != Vector2.zero)
        {
            playerMaster.PlayerAnim.SetBool("walking", true);
        }
        else
        {
            playerMaster.PlayerAnim.SetBool("walking", false);
        }
    }

    public void PlayerMeleeAttackAnimation()
    {
        playerMaster.PlayerAnim.SetTrigger("meleeAttack");
    }

    public void PlayerGrabObjectAnimation()
    {
        playerMaster.PlayerAnim.SetTrigger("grabObject");
    }

    public void TriggerAnimation(string triggerString)
    {
        playerMaster.PlayerAnim.SetTrigger(triggerString);
    }
}
