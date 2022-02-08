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
    [SerializeField] private Collider2D throwableCollider;
    [SerializeField] private SpriteRenderer throwableSr;

    private Rigidbody2D throwableRb;

    private void Awake()
    {
        throwableRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (beingThrown)
        {
            if (throwDirection.y > 1)
            {
                throwDirection.y -= Time.deltaTime;
            }
            else
            {
                throwDirection = Vector2.zero;
                throwableRb.velocity = Vector2.zero;
                beingThrown = false;
            }
        }
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
            PickupThrowable();
        }
        else
        {
            ThrowObject();
        }
        base.PerformInteraction();
    }

    private void PickupThrowable()
    {
        this.gameObject.transform.SetParent(player.playerItemHolder.transform);
        this.gameObject.transform.localPosition = Vector2.zero;
        throwableCollider.enabled = false;
        throwableSr.sortingLayerName = "Projectiles";
        canThrow = true;
    }

    private void ThrowObject()
    {
        this.gameObject.transform.SetParent(null);
        throwableCollider.enabled = true;
        throwDirection = player.FacingDirection;
        canThrow = false;
        beingThrown = true;
    }
}
