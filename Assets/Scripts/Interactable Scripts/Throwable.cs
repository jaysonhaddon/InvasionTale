using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactable
{

    [Header("Throwable Variables")]
    [SerializeField] private float throwSpeed;
    [SerializeField] private bool canThrow = false;
    [SerializeField] private bool beingThrownX = false;
    [SerializeField] private bool beingThrownY = false;
    [SerializeField] private Vector2 throwDirection;
    [SerializeField] private float yOffset;
    [SerializeField] private float minY;
    [SerializeField] private GameObject throwableCollider;

    [SerializeField] private float startTest;
    [SerializeField] private float test;
    [SerializeField] private float countDownOffset;
    [SerializeField] private float countDownTime;
    
    // Cached References
    private SpriteRenderer throwableSr;
    private Animator throwableAnim;
    private Rigidbody2D throwableRb;
    private Breakable throwableBreakable;

    private void Awake()
    {
        throwableSr = GetComponent<SpriteRenderer>();
        throwableAnim = GetComponent<Animator>();
        throwableRb = GetComponent<Rigidbody2D>();
        throwableBreakable = GetComponent<Breakable>();
        test = startTest;
    }

    private void Update()
    {
        if (beingThrownY)
        {
            if (throwDirection.y <= minY)
            {
                throwableRb.velocity = Vector2.zero;
                CancelInvoke();
                throwableSr.sortingLayerName = "Object";
                throwableBreakable.DeactivateObject();
                beingThrownY = false;
            }
        }
        else if (beingThrownX) 
        {
            test -= Time.deltaTime;
            if (test <= 0)
            {
                throwableRb.velocity = Vector2.zero;
                test = startTest;
                throwableSr.sortingLayerName = "Object";
                throwableBreakable.DeactivateObject();
                beingThrownX = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (beingThrownY)
        {
            throwableRb.velocity = new Vector2((throwDirection.x * throwSpeed), throwDirection.y);
        }
        else if (beingThrownX)
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
        throwableCollider.SetActive(false);
        throwableSr.sortingLayerName = "Projectiles";
        canThrow = true;
    }

    private void ThrowObject()
    {
        player.PlayerHeldObjectActions();
        this.gameObject.transform.SetParent(null);
        DetermineDirection(player.FacingDirection);
        Invoke("ReactivateCollision", .5f);
    }

    private void DetermineDirection(Vector2 facingDirection)
    {
        facingDirection = new Vector2(Mathf.Round(facingDirection.x), Mathf.Round(facingDirection.y));
        Debug.Log(facingDirection);
        if (facingDirection == Vector2.right || facingDirection == Vector2.left)
        {
            facingDirection.y = yOffset;
            throwDirection = new Vector2(facingDirection.x, facingDirection.y);
            beingThrownY = true;
            InvokeRepeating("OffsetCountdown", 0, countDownTime);
        }
        else
        {
            throwDirection = new Vector2(facingDirection.x, facingDirection.y);
            beingThrownX = true;
        }
        
    }

    private void OffsetCountdown()
    {
        throwDirection.y -= countDownOffset;
    }

    private void ReactivateCollision()
    {
        throwableCollider.SetActive(true);
        canThrow = false;
    }

    public void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }
}
