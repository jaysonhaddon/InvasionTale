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

    // Inspector References
    [SerializeField] private SpriteRenderer throwableSr;
    [SerializeField] private SpriteRenderer throwableShadowSr;
    [SerializeField] private Collider2D throwableCollider;
    [SerializeField] private GameObject destroyEffect;

    // Cached References
    private Animator throwableAnim;
    private Rigidbody2D throwableRb;
    private Breakable throwableBreakable;

    private void Awake()
    {
        throwableAnim = GetComponent<Animator>();
        throwableRb = GetComponent<Rigidbody2D>();
        throwableBreakable = GetComponent<Breakable>();
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
        throwDirection.Normalize();
        throwableAnim.SetTrigger("throw");
        beingThrown = true;
        canThrow = false;
        DisablePlayerInteraction();
    }

    // Called by the Animator if object does not collide with another object before end of throw
    public void DeactivateThrowable()
    {
        beingThrown = false;
        throwableRb.velocity = Vector2.zero;
        StartCoroutine(DeactivateCo());
    }

    private IEnumerator DeactivateCo()
    {
        throwableSr.enabled = false;
        throwableShadowSr.enabled = false;
        destroyEffect.transform.position = throwableCollider.transform.position;
        destroyEffect.SetActive(true);
        yield return new WaitForSeconds(deactivateTime);
        this.gameObject.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (beingThrown && other.gameObject.CompareTag(interactTags[1])) 
        {          
            Breakable breakable = other.GetComponent<Breakable>();
            throwableAnim.SetTrigger("idle");
            breakable.DeactivateObject();
            DeactivateThrowable();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!canThrow) 
        {
            base.OnTriggerExit2D(other);
        }
    }
}
