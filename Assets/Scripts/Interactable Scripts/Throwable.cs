using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactable
{

    [Header("Throwable Variables")]
    [SerializeField] private float throwSpeed;
    [SerializeField] private bool canThrow = false;
    [SerializeField] private bool beingThrown = false;
    [SerializeField] private Vector2 throwDirection;
    [SerializeField] private float deactivateTime;

    // Cached References
    [SerializeField] private SpriteRenderer throwableSr;
    [SerializeField] private SpriteRenderer throwableShadowSr;
    [SerializeField] private Collider2D throwableCollider;
    [SerializeField] private GameObject destroyEffect;
    private Animator throwableAnim;
    private Rigidbody2D throwableRb;
    private Breakable throwableBreakable;

    private void Awake()
    {
        throwableAnim = GetComponent<Animator>();
        throwableRb = GetComponent<Rigidbody2D>();
        throwableBreakable = GetComponent<Breakable>();
    }

    private void Update()
    {
        ThrowAnimationDirection();
    }

    private void FixedUpdate()
    {
        if (beingThrown)
        {
            throwableRb.velocity = throwDirection * throwSpeed;
        }
    }

    public override void PerformInteraction()
    {
        if (!canThrow)
        {
            PickupObject();
        }
        else
        {
            ThrowObject();
        }
    }

    private void PickupObject()
    {
        this.gameObject.transform.SetParent(player.playerItemHolder.transform);
        this.gameObject.transform.localPosition = Vector2.zero;
        player.PlayerHeldObjectActions();
        throwableCollider.enabled = false;
        throwableSr.sortingLayerName = "Projectiles";
        canThrow = true;
    }

    private void ThrowObject()
    {
        player.PlayerHeldObjectActions();
        this.gameObject.transform.SetParent(null);
        throwDirection = player.FacingDirection;
        beingThrown = true;
        throwableAnim.SetTrigger("throw");
        Invoke("ReactivateCollision", .5f);
    }

    private void ReactivateCollision()
    {
        throwableCollider.enabled = true;
        canThrow = false;
    }

    private void ThrowAnimationDirection()
    {
        throwableAnim.SetFloat("moveX", throwDirection.x);
        throwableAnim.SetFloat("moveY", throwDirection.y);
    }

    public void DeactivateThrowable()
    {
        beingThrown = false;
        throwableRb.velocity = Vector2.zero;
        StartCoroutine(DeactivateCo());
    }

        private IEnumerator DeactivateCo()
    {
        throwableCollider.enabled = false;
        throwableSr.enabled = false;
        throwableShadowSr.enabled = false;
        destroyEffect.transform.position = throwableCollider.transform.position;
        destroyEffect.SetActive(true);
        yield return new WaitForSeconds(deactivateTime);
        this.gameObject.SetActive(false);
    }
}
