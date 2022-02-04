using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    [Header("Melee Weapon Variables")]
    [SerializeField] private float attackThrust;
    [SerializeField] private float attackTime;
    [SerializeField] private float timeBtwAttacks;
    [SerializeField] private string[] damageTags;
    [SerializeField] GameObject attackEffect;

    // Cached References
    [SerializeField] private PlayerMaster playerMaster;
    private Animator weaponAnim;

    private void Awake()
    {
        playerMaster = GetComponentInParent<PlayerMaster>();
        weaponAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationDirection();
        if (playerMaster.CurrentState == PlayerState.meleeAttack && playerMaster.canAttack)
        {
            PerformAttack();
        }
    }

    public void PerformAttack()
    {
        StartCoroutine(AttackingCo());
    }

    IEnumerator AttackingCo()
    {
        weaponAnim.SetTrigger("meleeAttack");
        playerMaster.SetThrustSpeed(attackThrust);
        playerMaster.canAttack = false;
        yield return new WaitForSeconds(attackTime);
        playerMaster.CurrentState = PlayerState.idle;
        yield return new WaitForSeconds(timeBtwAttacks);
        playerMaster.canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(damageTags[0]))
        {
            Breakable breakableObject = other.GetComponent<Breakable>();
            breakableObject.StartCoroutine(breakableObject.DeactivateObject());
        }
    }

    private void AnimationDirection()
    {
        weaponAnim.SetFloat("attackX", playerMaster.FacingDirection.x);
        weaponAnim.SetFloat("attackY", playerMaster.FacingDirection.y);
    }
}
