using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 idleDirection;
    [SerializeField] private Vector2 moveDirection;

    [SerializeField] private Animator playerAnim;
    private Rigidbody2D playerRb;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
        PlayerWalkAnimation();
    }

    private void PlayerInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();
    }

    private void PlayerWalk()
    {
        playerRb.velocity = moveDirection * moveSpeed;
    }

    private void PlayerWalkAnimation()
    {
        playerAnim.SetFloat("moveX", moveDirection.x);
        playerAnim.SetFloat("moveY", moveDirection.y);
        playerAnim.SetFloat("idleX", idleDirection.x);
        playerAnim.SetFloat("idleY", idleDirection.y);

        if (moveDirection != Vector2.zero)
        {
            playerAnim.SetBool("walking", true);
            idleDirection = moveDirection;
        }
        else
        {
            playerAnim.SetBool("walking", false);
        }
    }
}