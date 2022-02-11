using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Cached References
    private PlayerMaster playerMaster;
    private Animator playerAnim;

    private void Awake()
    {
        playerMaster = GetComponent<PlayerMaster>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorVariables();
        if (playerMaster.CurrentState != PlayerState.meleeAttack && !playerMaster.holdingObject)
        {
            PlayerRunAnimation();
        }
        else if (playerMaster.holdingObject)
        {
            PlayerItemRunAnimation();
        }
    }

    private void AnimatorVariables()
    {
        playerAnim.SetFloat("moveX", playerMaster.MoveDirection.x);
        playerAnim.SetFloat("moveY", playerMaster.MoveDirection.y);
        playerAnim.SetFloat("idleX", playerMaster.FacingDirection.x);
        playerAnim.SetFloat("idleY", playerMaster.FacingDirection.y);
    }

    private void PlayerRunAnimation()
    {
        if (playerMaster.MoveDirection != Vector2.zero)
        {
            playerAnim.SetBool("walking", true);
        }
        else
        {
            playerAnim.SetBool("walking", false);
        }
    }

    private void PlayerItemRunAnimation()
    {
        if (playerMaster.MoveDirection != Vector2.zero)
        {
            playerAnim.SetBool("itemWalking", true);
        }
        else
        {
            playerAnim.SetBool("itemWalking", false);
        }
    }

    public void PlayerMeleeAttackAnimation()
    {
        playerAnim.SetTrigger("meleeAttack");
    }

    public void PlayerGrabObjectAnimation()
    {
        playerAnim.SetTrigger("grabObject");
    }

    public void PlayerThrowObjectAnimation()
    {
        playerAnim.SetTrigger("throwObject");
    }

    public void TriggerAnimation(string triggerString)
    {
        playerAnim.SetTrigger(triggerString);
    }
}
